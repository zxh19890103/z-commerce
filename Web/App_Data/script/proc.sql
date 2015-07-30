if exists (select * from sysobjects where id = object_id(N'[np_copygoods]') and [type]=N'P')
	drop procedure [np_copygoods]
go
create proc [np_copygoods]
(
@Id int=0
)
as
begin
	if not exists(select Id from nt_goods where id=@id)
		return;
  --copy goods' info----1
  insert into nt_goods
  (
    [GoodsClassId],
    [Name],
    [GoodsGuid],
    [Measure],
    [Price],
    [MarketPrice],
    [Cost],
    [OldPrice],
    [EnableVipPrice],
    [VipPrice],
    [EnableSpecialPrice],
    [SpecialPrice],
    [SpecialPriceStartDate],
    [SpecialPriceEndDate],
    [Rating],
    [SellNumber],
    [DisableBuyButton],
    [DisableWishlistButton],
    [OtherClass],
    [Points],
    [StockQuantity],
    [Weight],
    [Length],
    [Width],
    [Height],
    [SeoTitle],
    [SeoKeywords],
    [SeoDescription],
    [ShortDescription],
    [FullDescription],
    [Display],
    [DisplayOrder],
    [Deleted],
    [CreatedDate],
    [UpdatedDate],
    [UserId],
    [Recommended],
    [Hot],
    [SetTop],
    [IsNew],
    [Tags],
    [Guid],
    [PictureUrl],
    [PageNum],
    [DiscountId],
    [LanguageId],
    [BrandId],
	[Removed]
  )
  select
    [GoodsClassId],
    [Name]+N'-copy' as [Name],
    [GoodsGuid]+N'-copy' as [GoodsGuid],
    [Measure],
    [Price],
    [MarketPrice],
    [Cost],
    [OldPrice],
    [EnableVipPrice],
    [VipPrice],
    [EnableSpecialPrice],
    [SpecialPrice],
    [SpecialPriceStartDate],
    [SpecialPriceEndDate],
    [Rating],
    [SellNumber],
    [DisableBuyButton],
    [DisableWishlistButton],
    [OtherClass],
    [Points],
    [StockQuantity],
    [Weight],
    [Length],
    [Width],
    [Height],
    [SeoTitle],
    [SeoKeywords],
    [SeoDescription],
    [ShortDescription],
    [FullDescription],
    [Display],
    [DisplayOrder],
    [Deleted],
    [CreatedDate],
    [UpdatedDate],
    [UserId],
    [Recommended],
    [Hot],
    [SetTop],
    [IsNew],
    [Tags],
    newid() as [Guid],
    [PictureUrl],
    [PageNum],
    [DiscountId],
    [LanguageId],
	[BrandId],
	[Removed]
  from nt_goods where id=@Id;

  declare @newid int;
  set @newid=@@IDENTITY;

  --copy pictures of specified gooods---2
  insert into Nt_Goods_Picture_Mapping
  (GoodsId,PictureId)
  select @newid as GoodsId,PictureId
  from Nt_Goods_Picture_Mapping where GoodsId=@Id;
  
  --copy attribute of specified goods---3
  declare @AttrId int, @IsRequired bit,@ControlType int,@TextPrompt nvarchar(1024),@DisplayOrder int,@GoodsAttributeId int,@GoodsId int;
  declare mycursor cursor
  for 
  select id,IsRequired,ControlType,TextPrompt,DisplayOrder,GoodsAttributeId,GoodsId from Nt_Goods_Attribute_Mapping where  GoodsId=@Id;
  open mycursor
  fetch next from mycursor into @AttrId,@IsRequired,@ControlType,@TextPrompt,@DisplayOrder,@GoodsAttributeId,@GoodsId

  while (@@fetch_status=0)
  begin
    insert into Nt_Goods_Attribute_Mapping
    (IsRequired,ControlType,TextPrompt,DisplayOrder,GoodsAttributeId,GoodsId)
    values(@IsRequired,@ControlType,@TextPrompt,@DisplayOrder,@GoodsAttributeId,@newid);
    declare @tempid int;
	set @tempid=@@IDENTITY;

	--copy attributes values----3-1
	 insert into Nt_Goods_VariantAttributeValue
     (GoodsVariantAttributeId,Name,AttributeValue,Selected,PriceAdjustment,DisplayOrder)
     select @tempid as GoodsVariantAttributeId,Name,AttributeValue,Selected,PriceAdjustment,DisplayOrder
     from Nt_Goods_VariantAttributeValue where GoodsVariantAttributeId=@AttrId;
     fetch next from mycursor into @AttrId,@IsRequired,@ControlType,@TextPrompt,@DisplayOrder,@GoodsAttributeId,@GoodsId
  end
  close mycursor
  deallocate mycursor
    
  --copy parameters of specified goods---4
   insert into Nt_Goods_Parameter
  (GoodsParameterGroupId,GoodsId,Name,Value)
  select GoodsParameterGroupId,@newid as GoodsId,Name,Value
  from Nt_Goods_Parameter where GoodsId=@Id;

  --copy bound goods of specified goods---5
   insert into Nt_Goods_Binding
  (GoodsId,BindingId)
  select @newid as GoodsId,BindingId
  from Nt_Goods_Binding where GoodsId=@Id;
 end
 go

if exists (select * from sysobjects where id = object_id(N'[np_copyproduct]') and [type]=N'P')
	drop procedure [np_copyproduct]
go
create procedure [np_copyproduct]
(
@Id int=0
)
as
begin

	if not exists(select Id from Nt_Product where id=@id)
		return;

	--COPY PRODUCT INFO
	 insert into Nt_Product
    (
	[Name],
	[ShortDescription],
	[FullDescription],
	[Rating],
	[SetTop],
	[Recommended],
	[IsNew],
	[SeoTitle],
	[SeoKeywords],
	[SeoDescription],
	[UpdatedDate],
	[CreatedDate],
	[Display],
	[DisplayOrder],
	[ProductCategoryId],
	[UserId],
	[Thumb],
	[PictureUrl],
	[FileName],
	[FileUrl],
	[FileSize],
	[Guid],
	[AreaId],
	[Day],
	[F0],
	[F1],
	[F2],
	[F3],
	[F4],
	[F5],
	[F6],
	[F7],
	[F8],
	[F9],
	[F10],
	[F11],
	[F12],
	[F13],
	[F14],
	[F15],
	[Downloadable],
	[LanguageId],
	[Deleted]
	)
    select 
	[Name]+N'-copy' as [Name],
	[ShortDescription],
	[FullDescription],
	[Rating],
	[SetTop],
	[Recommended],
	[IsNew],
	[SeoTitle],
	[SeoKeywords],
	[SeoDescription],
	[UpdatedDate],
	[CreatedDate],
	[Display],
	[DisplayOrder],
	[ProductCategoryId],
	[UserId],
	[Thumb],
	[PictureUrl],
	[FileName],
	[FileUrl],
	[FileSize],
	newid() as [Guid],
	[AreaId],
	[Day],
	[F0],
	[F1],
	[F2],
	[F3],
	[F4],
	[F5],
	[F6],
	[F7],
	[F8],
	[F9],
	[F10],
	[F11],
	[F12],
	[F13],
	[F14],
	[F15],
	[Downloadable],
	[LanguageId],
	[Deleted]
    from Nt_Product where Id=@Id;

	declare @newid int;
	set @newid=@@IDENTITY;

	--COPY PICTURES OF SPECIFIIED PRODUCT
  insert into Nt_Product_Picture_Mapping
  (ProductId,PictureId)
  select @newid as ProductId,PictureId
  from Nt_Product_Picture_Mapping where ProductId=@Id;
end
go
if exists (select * from sysobjects where id = object_id(N'[np_copyarticle]') and [type]=N'P')
	drop procedure [np_copyarticle]
go
create procedure [np_copyarticle]
(
@Id int=0
)
as
begin

	if not exists(select Id from Nt_Article where id=@id)
		return;

	--COPY ARTICLE INFO
	 insert into Nt_Article
    (
	[Title],
	[Author],
	[Source],
	[Short],
	[Body],
	[Rating],
	[SetTop],
	[Recommended],
	[PictureUrl],
	[SeoKeywords],
	[SeoDescription],
	[UpdatedDate],
	[CreatedDate],
	[UserId],
	[Display],
	[DisplayOrder],
	[Guid],
	[FileUrl],
	[FileName],
	[FileSize],
	[ArticleClassId],
	[Downloadable],
	[LanguageId],
	[Deleted]
	)
    select 
	[Title]+N'-copy' as [Title],
	[Author],
	[Source],
	[Short],
	[Body],
	[Rating],
	[SetTop],
	[Recommended],
	[PictureUrl],
	[SeoKeywords],
	[SeoDescription],
	[UpdatedDate],
	[CreatedDate],
	[UserId],
	[Display],
	[DisplayOrder],
	newid() as [Guid],
	[FileUrl],
	[FileName],
	[FileSize],
	[ArticleClassId],
	[Downloadable],
	[LanguageId],
	[Deleted]
    from Nt_Article where Id=@Id;
end
go