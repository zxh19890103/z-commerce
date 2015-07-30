/*
{tab}:the name of specified table;
{targetID}:the id of the specified record you want to migrate;
{toParent}:the target record's id you want to migratet to.
*/
update {tab} set depth=depth-(select depth from {tab} where ID={targetID}) where crumbs like '%,{targetID},%';
update {tab} set crumbs=replace(crumbs,(select crumbs from {tab} where ID={targetID}),',{targetID},') where crumbs like '%,{targetID},%';
update {tab} set parent={toParent} where id={targetID};
GO
declare @baseDepth int;
declare @baseCrumbs varchar(1024);
if {toParent}>0
	begin
		set @baseDepth=(select depth from {tab} where ID={toParent});
		set @baseCrumbs=(select crumbs from {tab} where ID={toParent});
		set @baseCrumbs=SUBSTRING(@baseCrumbs,1,LEN(@baseCrumbs)-1);
	end
else
	begin
		set @baseDepth=0;
		set @baseCrumbs='0';
	end
update {tab} set depth=@baseDepth+depth+1 where crumbs like '%,{targetID},%';
update {tab} set crumbs=@baseCrumbs+crumbs where crumbs like '%,{targetID},%';
GO