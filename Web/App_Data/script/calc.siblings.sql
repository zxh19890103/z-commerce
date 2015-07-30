declare @id int,@title varchar(512),@pid int,@ptitle varchar(512),@nid int,@ntitle varchar(512);
declare mycursor cursor for
	select id,{namefield} from {tablename} where {filter} order by {orderby}
open mycursor;
fetch next from mycursor into @id,@title;
while @@FETCH_STATUS=0
	begin
		if @id={specifiedid}
			begin
				--next record
				fetch next from mycursor into @nid,@ntitle;
				break;
			end
		set @pid=@id;
		set @ptitle=@title;
		fetch next from mycursor into @id,@title;		
	end
close mycursor;
deallocate mycursor;
select @pid as PreId,@ptitle As PreTitle,@nid As NextId,@ntitle As NextTitle;