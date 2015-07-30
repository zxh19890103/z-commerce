--Drop All Views
--Drop View 'View_GoodsVariantAttributeValue' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_GoodsVariantAttributeValue]'))
    Drop VIEW [dbo].[View_GoodsVariantAttributeValue]
GO
--Drop View 'View_ShoppingCart' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_ShoppingCart]'))
    Drop VIEW [dbo].[View_ShoppingCart]
GO
--Drop View 'View_Article' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Article]'))
    Drop VIEW [dbo].[View_Article]
GO
--Drop View 'View_ArticleClass' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_ArticleClass]'))
    Drop VIEW [dbo].[View_ArticleClass]
GO
--Drop View 'View_Banner' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Banner]'))
    Drop VIEW [dbo].[View_Banner]
GO
--Drop View 'View_Customer_Wishlist' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Customer_Wishlist]'))
    Drop VIEW [dbo].[View_Customer_Wishlist]
GO
--Drop View 'View_FriendLink' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_FriendLink]'))
    Drop VIEW [dbo].[View_FriendLink]
GO
--Drop View 'View_Goods' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Goods]'))
    Drop VIEW [dbo].[View_Goods]
GO
--Drop View 'View_GoodsAttribute' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_GoodsAttribute]'))
    Drop VIEW [dbo].[View_GoodsAttribute]
GO
--Drop View 'View_GoodsClass' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_GoodsClass]'))
    Drop VIEW [dbo].[View_GoodsClass]
GO
--Drop View 'View_GoodsParameter' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_GoodsParameter]'))
    Drop VIEW [dbo].[View_GoodsParameter]
GO
--Drop View 'View_GoodsPicture' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_GoodsPicture]'))
    Drop VIEW [dbo].[View_GoodsPicture]
GO
--Drop View 'View_Customer' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Customer]'))
    Drop VIEW [dbo].[View_Customer]
GO
--Drop View 'View_Goods_Ask' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Goods_Ask]'))
    Drop VIEW [dbo].[View_Goods_Ask]
GO
--Drop View 'View_Goods_Binding' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Goods_Binding]'))
    Drop VIEW [dbo].[View_Goods_Binding]
GO
--Drop View 'View_Goods_Review' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Goods_Review]'))
    Drop VIEW [dbo].[View_Goods_Review]
GO
--Drop View 'View_GuestBook' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_GuestBook]'))
    Drop VIEW [dbo].[View_GuestBook]
GO
--Drop View 'View_GuestBookReply' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_GuestBookReply]'))
    Drop VIEW [dbo].[View_GuestBookReply]
GO
--Drop View 'View_Language' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Language]'))
    Drop VIEW [dbo].[View_Language]
GO
--Drop View 'View_Log' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Log]'))
    Drop VIEW [dbo].[View_Log]
GO
--Drop View 'View_Navigation' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Navigation]'))
    Drop VIEW [dbo].[View_Navigation]
GO
--Drop View 'View_Order' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Order]'))
    Drop VIEW [dbo].[View_Order]
GO
--Drop View 'View_OrderItem' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_OrderItem]'))
    Drop VIEW [dbo].[View_OrderItem]
GO
--Drop View 'View_Permission' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Permission]'))
    Drop VIEW [dbo].[View_Permission]
GO
--Drop View 'View_Product' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Product]'))
    Drop VIEW [dbo].[View_Product]
GO
--Drop View 'View_ProductCategory' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_ProductCategory]'))
    Drop VIEW [dbo].[View_ProductCategory]
GO
--Drop View 'View_ProductPicture' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_ProductPicture]'))
    Drop VIEW [dbo].[View_ProductPicture]
GO
--Drop View 'View_Slider' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Slider]'))
    Drop VIEW [dbo].[View_Slider]
GO
--Drop View 'View_User' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_User]'))
    Drop VIEW [dbo].[View_User]
GO
--Drop View 'View_User_Permission' 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_User_Permission]'))
    Drop VIEW [dbo].[View_User_Permission]
GO


--Drop All Foreign Key...
--drop Foreign Key 'FK_Nt_Article_Nt_Article_Class' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Article_Nt_Article_Class]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Article] DROP CONSTRAINT [FK_Nt_Article_Nt_Article_Class];
GO
--drop Foreign Key 'FK_Nt_Article_Nt_Language' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Article_Nt_Language]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Article] DROP CONSTRAINT [FK_Nt_Article_Nt_Language];
GO
--drop Foreign Key 'FK_Nt_Banner_Nt_Language' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Banner_Nt_Language]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Banner] DROP CONSTRAINT [FK_Nt_Banner_Nt_Language];
GO
--drop Foreign Key 'FK_Nt_Brand_Nt_Language' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Brand_Nt_Language]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Brand] DROP CONSTRAINT [FK_Nt_Brand_Nt_Language];
GO
--drop Foreign Key 'FK_Nt_Customer_Nt_CustomerRole' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Customer_Nt_CustomerRole]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Customer] DROP CONSTRAINT [FK_Nt_Customer_Nt_CustomerRole];
GO
--drop Foreign Key 'FK_Nt_Customer_Consignee_Nt_Customer' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Customer_Consignee_Nt_Customer]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Customer_Consignee] DROP CONSTRAINT [FK_Nt_Customer_Consignee_Nt_Customer];
GO
--drop Foreign Key 'FK_Nt_Customer_Message_Nt_Customer' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Customer_Message_Nt_Customer]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Customer_Message] DROP CONSTRAINT [FK_Nt_Customer_Message_Nt_Customer];
GO
--drop Foreign Key 'FK_Nt_Customer_Wishlist_Nt_Customer' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Customer_Wishlist_Nt_Customer]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Customer_Wishlist] DROP CONSTRAINT [FK_Nt_Customer_Wishlist_Nt_Customer];
GO
--drop Foreign Key 'FK_Nt_Customer_Wishlist_Nt_Goods' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Customer_Wishlist_Nt_Goods]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Customer_Wishlist] DROP CONSTRAINT [FK_Nt_Customer_Wishlist_Nt_Goods];
GO
--drop Foreign Key 'FK_Nt_FriendLink_Nt_Language' 
IF OBJECT_ID(N'[dbo].[FK_Nt_FriendLink_Nt_Language]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_FriendLink] DROP CONSTRAINT [FK_Nt_FriendLink_Nt_Language];
GO
--drop Foreign Key 'FK_Nt_Goods_Nt_Goods_Class' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Goods_Nt_Goods_Class]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Goods] DROP CONSTRAINT [FK_Nt_Goods_Nt_Goods_Class];
GO
--drop Foreign Key 'FK_Nt_Goods_Nt_Language' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Goods_Nt_Language]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Goods] DROP CONSTRAINT [FK_Nt_Goods_Nt_Language];
GO
--drop Foreign Key 'FK_Nt_Goods_Parameter_Nt_Goods_ParameterGroup' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Goods_Parameter_Nt_Goods_ParameterGroup]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Goods_Parameter] DROP CONSTRAINT [FK_Nt_Goods_Parameter_Nt_Goods_ParameterGroup];
GO
--drop Foreign Key 'FK_Nt_Goods_Parameter_Nt_Goods' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Goods_Parameter_Nt_Goods]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Goods_Parameter] DROP CONSTRAINT [FK_Nt_Goods_Parameter_Nt_Goods];
GO
--drop Foreign Key 'FK_Nt_Goods_Ask_Nt_Goods' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Goods_Ask_Nt_Goods]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Goods_Ask] DROP CONSTRAINT [FK_Nt_Goods_Ask_Nt_Goods];
GO
--drop Foreign Key 'FK_Nt_Goods_Ask_Nt_Customer' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Goods_Ask_Nt_Customer]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Goods_Ask] DROP CONSTRAINT [FK_Nt_Goods_Ask_Nt_Customer];
GO
--drop Foreign Key 'FK_Nt_Goods_Attribute_Mapping_Nt_GoodsAttribute' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Goods_Attribute_Mapping_Nt_GoodsAttribute]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Goods_Attribute_Mapping] DROP CONSTRAINT [FK_Nt_Goods_Attribute_Mapping_Nt_GoodsAttribute];
GO
--drop Foreign Key 'FK_Nt_Goods_Attribute_Mapping_Nt_Goods' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Goods_Attribute_Mapping_Nt_Goods]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Goods_Attribute_Mapping] DROP CONSTRAINT [FK_Nt_Goods_Attribute_Mapping_Nt_Goods];
GO
--drop Foreign Key 'FK_Nt_Goods_Promotion_Mapping_Nt_Goods' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Goods_Promotion_Mapping_Nt_Goods]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Goods_Promotion_Mapping] DROP CONSTRAINT [FK_Nt_Goods_Promotion_Mapping_Nt_Goods];
GO
--drop Foreign Key 'FK_Nt_Goods_Promotion_Mapping_Nt_Goods_Promotion' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Goods_Promotion_Mapping_Nt_Goods_Promotion]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Goods_Promotion_Mapping] DROP CONSTRAINT [FK_Nt_Goods_Promotion_Mapping_Nt_Goods_Promotion];
GO
--drop Foreign Key 'FK_Nt_Goods_VariantAttributeValue_Nt_Goods_Attribute_Mapping' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Goods_VariantAttributeValue_Nt_Goods_Attribute_Mapping]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Goods_VariantAttributeValue] DROP CONSTRAINT [FK_Nt_Goods_VariantAttributeValue_Nt_Goods_Attribute_Mapping];
GO
--drop Foreign Key 'FK_Nt_Goods_Binding_Nt_Goods' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Goods_Binding_Nt_Goods]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Goods_Binding] DROP CONSTRAINT [FK_Nt_Goods_Binding_Nt_Goods];
GO
--drop Foreign Key 'FK_Nt_Goods_Picture_Mapping_Nt_Goods' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Goods_Picture_Mapping_Nt_Goods]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Goods_Picture_Mapping] DROP CONSTRAINT [FK_Nt_Goods_Picture_Mapping_Nt_Goods];
GO
--drop Foreign Key 'FK_Nt_Goods_Picture_Mapping_Nt_Picture' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Goods_Picture_Mapping_Nt_Picture]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Goods_Picture_Mapping] DROP CONSTRAINT [FK_Nt_Goods_Picture_Mapping_Nt_Picture];
GO
--drop Foreign Key 'FK_Nt_Goods_Review_Nt_Customer' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Goods_Review_Nt_Customer]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Goods_Review] DROP CONSTRAINT [FK_Nt_Goods_Review_Nt_Customer];
GO
--drop Foreign Key 'FK_Nt_Goods_Review_Nt_Goods' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Goods_Review_Nt_Goods]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Goods_Review] DROP CONSTRAINT [FK_Nt_Goods_Review_Nt_Goods];
GO
--drop Foreign Key 'FK_Nt_Goods_Tag_Nt_Language' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Goods_Tag_Nt_Language]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Goods_Tag] DROP CONSTRAINT [FK_Nt_Goods_Tag_Nt_Language];
GO
--drop Foreign Key 'FK_Nt_GuestBook_Nt_Language' 
IF OBJECT_ID(N'[dbo].[FK_Nt_GuestBook_Nt_Language]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_GuestBook] DROP CONSTRAINT [FK_Nt_GuestBook_Nt_Language];
GO
--drop Foreign Key 'FK_Nt_GuestBookReply_Nt_GuestBook' 
IF OBJECT_ID(N'[dbo].[FK_Nt_GuestBookReply_Nt_GuestBook]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_GuestBookReply] DROP CONSTRAINT [FK_Nt_GuestBookReply_Nt_GuestBook];
GO
--drop Foreign Key 'FK_Nt_Measure_Nt_Language' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Measure_Nt_Language]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Measure] DROP CONSTRAINT [FK_Nt_Measure_Nt_Language];
GO
--drop Foreign Key 'FK_Nt_Order_Nt_Customer' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Order_Nt_Customer]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Order] DROP CONSTRAINT [FK_Nt_Order_Nt_Customer];
GO
--drop Foreign Key 'FK_Nt_OrderItem_Nt_Order' 
IF OBJECT_ID(N'[dbo].[FK_Nt_OrderItem_Nt_Order]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_OrderItem] DROP CONSTRAINT [FK_Nt_OrderItem_Nt_Order];
GO
--drop Foreign Key 'FK_Nt_OrderNote_Nt_Order' 
IF OBJECT_ID(N'[dbo].[FK_Nt_OrderNote_Nt_Order]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_OrderNote] DROP CONSTRAINT [FK_Nt_OrderNote_Nt_Order];
GO
--drop Foreign Key 'FK_Nt_Permission_UserLevel_Mapping_Nt_Permission' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Permission_UserLevel_Mapping_Nt_Permission]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Permission_UserLevel_Mapping] DROP CONSTRAINT [FK_Nt_Permission_UserLevel_Mapping_Nt_Permission];
GO
--drop Foreign Key 'FK_Nt_Permission_UserLevel_Mapping_Nt_UserLevel' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Permission_UserLevel_Mapping_Nt_UserLevel]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Permission_UserLevel_Mapping] DROP CONSTRAINT [FK_Nt_Permission_UserLevel_Mapping_Nt_UserLevel];
GO
--drop Foreign Key 'FK_Nt_Product_Nt_ProductCategory' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Product_Nt_ProductCategory]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Product] DROP CONSTRAINT [FK_Nt_Product_Nt_ProductCategory];
GO
--drop Foreign Key 'FK_Nt_Product_Nt_Language' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Product_Nt_Language]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Product] DROP CONSTRAINT [FK_Nt_Product_Nt_Language];
GO
--drop Foreign Key 'FK_Nt_Product_Picture_Mapping_Nt_Product' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Product_Picture_Mapping_Nt_Product]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Product_Picture_Mapping] DROP CONSTRAINT [FK_Nt_Product_Picture_Mapping_Nt_Product];
GO
--drop Foreign Key 'FK_Nt_Product_Picture_Mapping_Nt_Picture' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Product_Picture_Mapping_Nt_Picture]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Product_Picture_Mapping] DROP CONSTRAINT [FK_Nt_Product_Picture_Mapping_Nt_Picture];
GO
--drop Foreign Key 'FK_Nt_ShoppingCartItem_Nt_Customer' 
IF OBJECT_ID(N'[dbo].[FK_Nt_ShoppingCartItem_Nt_Customer]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_ShoppingCartItem] DROP CONSTRAINT [FK_Nt_ShoppingCartItem_Nt_Customer];
GO
--drop Foreign Key 'FK_Nt_ShoppingCartItem_Nt_Goods' 
IF OBJECT_ID(N'[dbo].[FK_Nt_ShoppingCartItem_Nt_Goods]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_ShoppingCartItem] DROP CONSTRAINT [FK_Nt_ShoppingCartItem_Nt_Goods];
GO
--drop Foreign Key 'FK_Nt_SinglePage_Nt_Language' 
IF OBJECT_ID(N'[dbo].[FK_Nt_SinglePage_Nt_Language]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_SinglePage] DROP CONSTRAINT [FK_Nt_SinglePage_Nt_Language];
GO
--drop Foreign Key 'FK_Nt_Slider_Nt_Language' 
IF OBJECT_ID(N'[dbo].[FK_Nt_Slider_Nt_Language]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_Slider] DROP CONSTRAINT [FK_Nt_Slider_Nt_Language];
GO
--drop Foreign Key 'FK_Nt_User_Nt_UserLevel' 
IF OBJECT_ID(N'[dbo].[FK_Nt_User_Nt_UserLevel]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_User] DROP CONSTRAINT [FK_Nt_User_Nt_UserLevel];
GO
--drop Foreign Key 'FK_Nt_WebsiteLinkItem_Nt_Language' 
IF OBJECT_ID(N'[dbo].[FK_Nt_WebsiteLinkItem_Nt_Language]','F') IS NOT NULL
  ALTER TABLE [dbo].[Nt_WebsiteLinkItem] DROP CONSTRAINT [FK_Nt_WebsiteLinkItem_Nt_Language];
GO


--Drop All tables...
IF OBJECT_ID(N'[dbo].[Nt_Area]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Area];
GO

IF OBJECT_ID(N'[dbo].[Nt_Article]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Article];
GO

IF OBJECT_ID(N'[dbo].[Nt_Article_Class]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Article_Class];
GO

IF OBJECT_ID(N'[dbo].[Nt_Banner]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Banner];
GO

IF OBJECT_ID(N'[dbo].[Nt_Brand]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Brand];
GO

IF OBJECT_ID(N'[dbo].[Nt_Campaign]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Campaign];
GO

IF OBJECT_ID(N'[dbo].[Nt_Customer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Customer];
GO

IF OBJECT_ID(N'[dbo].[Nt_CustomerRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_CustomerRole];
GO

IF OBJECT_ID(N'[dbo].[Nt_Customer_Consignee]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Customer_Consignee];
GO

IF OBJECT_ID(N'[dbo].[Nt_Customer_Message]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Customer_Message];
GO

IF OBJECT_ID(N'[dbo].[Nt_Customer_Wishlist]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Customer_Wishlist];
GO

IF OBJECT_ID(N'[dbo].[Nt_Day]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Day];
GO

IF OBJECT_ID(N'[dbo].[Nt_Discount]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Discount];
GO

IF OBJECT_ID(N'[dbo].[Nt_EmailAccount]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_EmailAccount];
GO

IF OBJECT_ID(N'[dbo].[Nt_Faq]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Faq];
GO

IF OBJECT_ID(N'[dbo].[Nt_Filter]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Filter];
GO

IF OBJECT_ID(N'[dbo].[Nt_FriendLink]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_FriendLink];
GO

IF OBJECT_ID(N'[dbo].[Nt_Goods]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Goods];
GO

IF OBJECT_ID(N'[dbo].[Nt_GoodsAttribute]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_GoodsAttribute];
GO

IF OBJECT_ID(N'[dbo].[Nt_Goods_Parameter]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Goods_Parameter];
GO

IF OBJECT_ID(N'[dbo].[Nt_Goods_Ask]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Goods_Ask];
GO

IF OBJECT_ID(N'[dbo].[Nt_Goods_Attribute_Mapping]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Goods_Attribute_Mapping];
GO

IF OBJECT_ID(N'[dbo].[Nt_Goods_Promotion_Mapping]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Goods_Promotion_Mapping];
GO

IF OBJECT_ID(N'[dbo].[Nt_Goods_VariantAttributeValue]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Goods_VariantAttributeValue];
GO

IF OBJECT_ID(N'[dbo].[Nt_Goods_Binding]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Goods_Binding];
GO

IF OBJECT_ID(N'[dbo].[Nt_Goods_Class]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Goods_Class];
GO

IF OBJECT_ID(N'[dbo].[Nt_Goods_ParameterGroup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Goods_ParameterGroup];
GO

IF OBJECT_ID(N'[dbo].[Nt_Goods_Picture_Mapping]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Goods_Picture_Mapping];
GO

IF OBJECT_ID(N'[dbo].[Nt_Goods_Promotion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Goods_Promotion];
GO

IF OBJECT_ID(N'[dbo].[Nt_Goods_Review]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Goods_Review];
GO

IF OBJECT_ID(N'[dbo].[Nt_Goods_Tag]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Goods_Tag];
GO

IF OBJECT_ID(N'[dbo].[Nt_GuestBook]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_GuestBook];
GO

IF OBJECT_ID(N'[dbo].[Nt_GuestBookReply]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_GuestBookReply];
GO

IF OBJECT_ID(N'[dbo].[Nt_Language]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Language];
GO

IF OBJECT_ID(N'[dbo].[Nt_Log]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Log];
GO

IF OBJECT_ID(N'[dbo].[Nt_Measure]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Measure];
GO

IF OBJECT_ID(N'[dbo].[Nt_Navigation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Navigation];
GO

IF OBJECT_ID(N'[dbo].[Nt_Order]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Order];
GO

IF OBJECT_ID(N'[dbo].[Nt_OrderItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_OrderItem];
GO

IF OBJECT_ID(N'[dbo].[Nt_OrderNote]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_OrderNote];
GO

IF OBJECT_ID(N'[dbo].[Nt_PaymentMethod]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_PaymentMethod];
GO

IF OBJECT_ID(N'[dbo].[Nt_Permission]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Permission];
GO

IF OBJECT_ID(N'[dbo].[Nt_Permission_UserLevel_Mapping]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Permission_UserLevel_Mapping];
GO

IF OBJECT_ID(N'[dbo].[Nt_Picture]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Picture];
GO

IF OBJECT_ID(N'[dbo].[Nt_Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Product];
GO

IF OBJECT_ID(N'[dbo].[Nt_ProductCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_ProductCategory];
GO

IF OBJECT_ID(N'[dbo].[Nt_Product_Picture_Mapping]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Product_Picture_Mapping];
GO

IF OBJECT_ID(N'[dbo].[Nt_ShippingMethod]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_ShippingMethod];
GO

IF OBJECT_ID(N'[dbo].[Nt_ShoppingCartItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_ShoppingCartItem];
GO

IF OBJECT_ID(N'[dbo].[Nt_SinglePage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_SinglePage];
GO

IF OBJECT_ID(N'[dbo].[Nt_Slider]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_Slider];
GO

IF OBJECT_ID(N'[dbo].[Nt_User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_User];
GO

IF OBJECT_ID(N'[dbo].[Nt_UserLevel]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_UserLevel];
GO

IF OBJECT_ID(N'[dbo].[Nt_WebsiteLinkItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nt_WebsiteLinkItem];
GO



--Create All tables...
--Creating Table 'Nt_Area' 
Create Table [dbo].[Nt_Area] (
    [Name] nvarchar(1024) not null,
    [EnglishName] nvarchar(1024) not null,
    [Description] nvarchar(1024) not null,
    [DisplayOrder] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Article' 
Create Table [dbo].[Nt_Article] (
    [Title] nvarchar(512) not null,
    [Author] nvarchar(256) not null,
    [Source] nvarchar(256) not null,
    [Short] nvarchar(1024) not null,
    [Body] nvarchar(max) not null,
    [Rating] int not null,
    [SetTop] bit not null,
    [Recommended] bit not null,
    [PictureUrl] nvarchar(256) not null,
    [SeoKeywords] nvarchar(1024) not null,
    [SeoDescription] nvarchar(1024) not null,
    [UpdatedDate] datetime not null,
    [CreatedDate] datetime not null,
    [UserId] int not null,
    [Display] bit not null,
    [DisplayOrder] int not null,
    [Guid] uniqueidentifier not null,
    [FileUrl] nvarchar(256) not null,
    [FileName] nvarchar(256) not null,
    [FileSize] nvarchar(256) not null,
    [ArticleClassId] int not null,
    [Downloadable] bit not null,
    [LanguageId] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Article_Class' 
Create Table [dbo].[Nt_Article_Class] (
    [LanguageId] int not null,
    [SeoTitle] nvarchar(512) not null,
    [SeoKeywords] nvarchar(1024) not null,
    [SeoDescription] nvarchar(1024) not null,
    [PictureUrl] nvarchar(256) not null,
    [Description] nvarchar(max) not null,
    [ListTemplate] nvarchar(256) not null,
    [DetailTemplate] nvarchar(256) not null,
    [Depth] int not null,
    [Parent] int not null,
    [Crumbs] nvarchar(512) not null,
    [Name] nvarchar(256) not null,
    [Display] bit not null,
    [DisplayOrder] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Banner' 
Create Table [dbo].[Nt_Banner] (
    [PictureUrl] nvarchar(256) not null,
    [Url] nvarchar(256) not null,
    [Title] nvarchar(512) not null,
    [Text] nvarchar(1024) not null,
    [Display] bit not null,
    [DisplayOrder] int not null,
    [LanguageId] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Brand' 
Create Table [dbo].[Nt_Brand] (
    [Name] nvarchar(256) not null,
    [PictureUrl] nvarchar(256) not null,
    [Description] nvarchar(1024) not null,
    [Url] nvarchar(256) not null,
    [Display] bit not null,
    [DisplayOrder] int not null,
    [LanguageId] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Campaign' 
Create Table [dbo].[Nt_Campaign] (
    [Name] nvarchar(256) not null,
    [Subject] nvarchar(512) not null,
    [Body] nvarchar(max) not null,
    [CreatedDate] datetime not null,
    [Display] bit not null,
    [DisplayOrder] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Customer' 
Create Table [dbo].[Nt_Customer] (
    [CustomerRoleId] int not null,
    [Name] nvarchar(256) not null,
    [Password] nvarchar(1024) not null,
    [Email] nvarchar(100) not null,
    [Points] int not null,
    [CreatedDate] datetime not null,
    [RealName] nvarchar(100) not null,
    [Address] nvarchar(256) not null,
    [Phone] nvarchar(50) not null,
    [Mobile] nvarchar(50) not null,
    [Zip] nvarchar(50) not null,
    [Active] bit not null,
    [BirthDay] datetime not null,
    [Gender] bit not null,
    [LastLoginIP] nvarchar(1024) not null,
    [LastLoginDate] datetime not null,
    [LoginTimes] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_CustomerRole' 
Create Table [dbo].[Nt_CustomerRole] (
    [Name] nvarchar(256) not null,
    [MeetPoints] int not null,
    [Note] nvarchar(1024) not null,
    [Active] bit not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Customer_Consignee' 
Create Table [dbo].[Nt_Customer_Consignee] (
    [CustomerId] int not null,
    [Name] nvarchar(256) not null,
    [Phone] nvarchar(50) not null,
    [Mobile] nvarchar(50) not null,
    [Email] nvarchar(100) not null,
    [Address] nvarchar(256) not null,
    [Zip] nvarchar(50) not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Customer_Message' 
Create Table [dbo].[Nt_Customer_Message] (
    [CustomerId] int not null,
    [Title] nvarchar(256) not null,
    [Body] nvarchar(1024) not null,
    [CreatedDate] nvarchar(1024) not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Customer_Wishlist' 
Create Table [dbo].[Nt_Customer_Wishlist] (
    [CustomerId] int not null,
    [GoodsId] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Day' 
Create Table [dbo].[Nt_Day] (
    [Title] nvarchar(1024) not null,
    [EnglishTitle] nvarchar(1024) not null,
    [Description] nvarchar(1024) not null,
    [DisplayOrder] int not null,
    [Date] datetime not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Discount' 
Create Table [dbo].[Nt_Discount] (
    [Name] nvarchar(1024) not null,
    [Description] nvarchar(1024) not null,
    [UsePercentage] bit not null,
    [DiscountPercentage] decimal(18,4) not null,
    [DiscountAmount] decimal(18,4) not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_EmailAccount' 
Create Table [dbo].[Nt_EmailAccount] (
    [Email] nvarchar(100) not null,
    [DisplayName] nvarchar(256) not null,
    [Host] nvarchar(256) not null,
    [Port] int not null,
    [UserName] nvarchar(256) not null,
    [Password] nvarchar(256) not null,
    [EnableSsl] bit not null,
    [UseDefaultCredentials] bit not null,
    [IsDefault] bit not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Faq' 
Create Table [dbo].[Nt_Faq] (
    [Question] nvarchar(1024) not null,
    [Answer] nvarchar(1024) not null,
    [Display] bit not null,
    [DisplayOrder] int not null,
    [Type] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Filter' 
Create Table [dbo].[Nt_Filter] (
    [Query] nvarchar(max) not null,
    [CookieID] nvarchar(256) not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_FriendLink' 
Create Table [dbo].[Nt_FriendLink] (
    [Url] nvarchar(256) not null,
    [PictureUrl] nvarchar(256) not null,
    [Title] nvarchar(512) not null,
    [Rating] int not null,
    [Display] bit not null,
    [DisplayOrder] int not null,
    [Area] int not null,
    [LanguageId] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Goods' 
Create Table [dbo].[Nt_Goods] (
    [GoodsClassId] int not null,
    [Name] nvarchar(512) not null,
    [GoodsGuid] nvarchar(1024) not null,
    [Measure] nvarchar(100) not null,
    [Price] decimal(18,4) not null,
    [MarketPrice] decimal(18,4) not null,
    [Cost] decimal(18,4) not null,
    [OldPrice] decimal(18,4) not null,
    [EnableVipPrice] bit not null,
    [VipPrice] decimal(18,4) not null,
    [EnableSpecialPrice] bit not null,
    [SpecialPrice] decimal(18,4) not null,
    [SpecialPriceStartDate] datetime not null,
    [SpecialPriceEndDate] datetime not null,
    [Rating] int not null,
    [SellNumber] int not null,
    [DisableBuyButton] bit not null,
    [DisableWishlistButton] bit not null,
    [OtherClass] nvarchar(1024) not null,
    [Points] int not null,
    [StockQuantity] int not null,
    [Weight] decimal(18,4) not null,
    [Length] decimal(18,4) not null,
    [Width] decimal(18,4) not null,
    [Height] decimal(18,4) not null,
    [SeoTitle] nvarchar(512) not null,
    [SeoKeywords] nvarchar(1024) not null,
    [SeoDescription] nvarchar(1024) not null,
    [ShortDescription] nvarchar(max) not null,
    [FullDescription] nvarchar(max) not null,
    [Display] bit not null,
    [DisplayOrder] int not null,
    [Deleted] bit not null,
    [CreatedDate] datetime not null,
    [UpdatedDate] datetime not null,
    [UserId] int not null,
    [Recommended] bit not null,
    [Hot] bit not null,
    [SetTop] bit not null,
    [IsNew] bit not null,
    [Tags] nvarchar(1024) not null,
    [Guid] uniqueidentifier not null,
    [PictureUrl] nvarchar(512) not null,
    [PageNum] int not null,
    [DiscountId] int not null,
    [BrandId] int not null,
    [LanguageId] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_GoodsAttribute' 
Create Table [dbo].[Nt_GoodsAttribute] (
    [Name] nvarchar(256) not null,
    [Description] nvarchar(max) not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Goods_Parameter' 
Create Table [dbo].[Nt_Goods_Parameter] (
    [GoodsParameterGroupId] int not null,
    [GoodsId] int not null,
    [Name] nvarchar(256) not null,
    [Value] nvarchar(max) not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Goods_Ask' 
Create Table [dbo].[Nt_Goods_Ask] (
    [GoodsId] int not null,
    [CustomerId] int not null,
    [Type] nvarchar(256) not null,
    [Title] nvarchar(512) not null,
    [Body] nvarchar(1024) not null,
    [Note] nvarchar(1024) not null,
    [CreatedDate] datetime not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Goods_Attribute_Mapping' 
Create Table [dbo].[Nt_Goods_Attribute_Mapping] (
    [IsRequired] bit not null,
    [ControlType] int not null,
    [TextPrompt] nvarchar(1024) not null,
    [DisplayOrder] int not null,
    [GoodsAttributeId] int not null,
    [GoodsId] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Goods_Promotion_Mapping' 
Create Table [dbo].[Nt_Goods_Promotion_Mapping] (
    [GoodsId] int not null,
    [PromotionId] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Goods_VariantAttributeValue' 
Create Table [dbo].[Nt_Goods_VariantAttributeValue] (
    [GoodsVariantAttributeId] int not null,
    [Name] nvarchar(400) not null,
    [AttributeValue] nvarchar(256) not null,
    [Selected] bit not null,
    [PriceAdjustment] decimal(18,4) not null,
    [DisplayOrder] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Goods_Binding' 
Create Table [dbo].[Nt_Goods_Binding] (
    [GoodsId] int not null,
    [BindingId] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Goods_Class' 
Create Table [dbo].[Nt_Goods_Class] (
    [PictureUrl] nvarchar(256) not null,
    [Description] nvarchar(max) not null,
    [SeoTitle] nvarchar(256) not null,
    [SeoKeywords] nvarchar(1024) not null,
    [SeoDescription] nvarchar(1024) not null,
    [LanguageId] int not null,
    [ListTemplate] nvarchar(256) not null,
    [DetailTemplate] nvarchar(256) not null,
    [Depth] int not null,
    [Parent] int not null,
    [Crumbs] nvarchar(512) not null,
    [Name] nvarchar(256) not null,
    [Display] bit not null,
    [DisplayOrder] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Goods_ParameterGroup' 
Create Table [dbo].[Nt_Goods_ParameterGroup] (
    [GroupName] nvarchar(256) not null,
    [Description] nvarchar(1024) not null,
    [Display] bit not null,
    [DisplayOrder] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Goods_Picture_Mapping' 
Create Table [dbo].[Nt_Goods_Picture_Mapping] (
    [GoodsId] int not null,
    [PictureId] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Goods_Promotion' 
Create Table [dbo].[Nt_Goods_Promotion] (
    [Tag] nvarchar(256) not null,
    [Description] nvarchar(1024) not null,
    [IsTimeLimited] bit not null,
    [StartDate] datetime not null,
    [EndDate] datetime not null,
    [DiscountPrice] decimal(18,4) not null,
    [UsePoint] int not null,
    [IsUsePoint] bit not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Goods_Review' 
Create Table [dbo].[Nt_Goods_Review] (
    [CustomerId] int not null,
    [GoodsId] int not null,
    [IsApproved] bit not null,
    [Title] nvarchar(256) not null,
    [Body] nvarchar(1024) not null,
    [Rating] int not null,
    [CreatedDate] datetime not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Goods_Tag' 
Create Table [dbo].[Nt_Goods_Tag] (
    [Tag] nvarchar(256) not null,
    [Display] bit not null,
    [DisplayOrder] int not null,
    [CreatedDate] datetime not null,
    [LanguageId] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_GuestBook' 
Create Table [dbo].[Nt_GuestBook] (
    [Title] nvarchar(1024) not null,
    [Name] nvarchar(256) not null,
    [Tel] nvarchar(50) not null,
    [Gender] bit not null,
    [NativePlace] nvarchar(256) not null,
    [Nation] nvarchar(256) not null,
    [PersonID] nvarchar(50) not null,
    [EduDegree] nvarchar(50) not null,
    [ZipCode] nvarchar(50) not null,
    [PoliticalRole] nvarchar(50) not null,
    [Address] nvarchar(256) not null,
    [GraduatedFrom] nvarchar(256) not null,
    [Grade] nvarchar(256) not null,
    [BirthDate] datetime not null,
    [Mobile] nvarchar(50) not null,
    [Email] nvarchar(50) not null,
    [Company] nvarchar(256) not null,
    [Body] nvarchar(1024) not null,
    [Display] bit not null,
    [DisplayOrder] int not null,
    [CreatedDate] datetime not null,
    [Note] nvarchar(1024) not null,
    [Type] int not null,
    [Viewed] bit not null,
    [LanguageId] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_GuestBookReply' 
Create Table [dbo].[Nt_GuestBookReply] (
    [ReplyContent] nvarchar(1024) not null,
    [GuestBookId] int not null,
    [ReplyDate] datetime not null,
    [ReplyMan] nvarchar(256) not null,
    [Display] bit not null,
    [DisplayOrder] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Language' 
Create Table [dbo].[Nt_Language] (
    [LanguageCode] nvarchar(10) not null,
    [Published] bit not null,
    [DisplayOrder] int not null,
    [Name] nvarchar(256) not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Log' 
Create Table [dbo].[Nt_Log] (
    [UserID] int not null,
    [LoginIP] nvarchar(50) not null,
    [Description] nvarchar(1024) not null,
    [RawUrl] nvarchar(256) not null,
    [CreatedDate] datetime not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Measure' 
Create Table [dbo].[Nt_Measure] (
    [Name] nvarchar(256) not null,
    [Description] nvarchar(512) not null,
    [Display] bit not null,
    [DisplayOrder] int not null,
    [LanguageId] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Navigation' 
Create Table [dbo].[Nt_Navigation] (
    [Path] nvarchar(100) not null,
    [AnchorTarget] nvarchar(20) not null,
    [LanguageId] int not null,
    [SeoTitle] nvarchar(512) not null,
    [SeoKeywords] nvarchar(1024) not null,
    [SeoDescription] nvarchar(1024) not null,
    [NaviType] int not null,
    [Depth] int not null,
    [Parent] int not null,
    [Crumbs] nvarchar(512) not null,
    [Name] nvarchar(256) not null,
    [Display] bit not null,
    [DisplayOrder] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Order' 
Create Table [dbo].[Nt_Order] (
    [OrderGuid] uniqueidentifier not null,
    [CustomerId] int not null,
    [CustomerIp] nvarchar(1024) not null,
    [CustomerConsigneeId] int not null,
    [Status] int not null,
    [ShippingStatus] int not null,
    [PaymentStatus] int not null,
    [PaymentMethodId] int not null,
    [ShippingMethodId] int not null,
    [OrderTotal] decimal(18,4) not null,
    [OrderTotalDiscount] decimal(18,4) not null,
    [RefundedAmount] decimal(18,4) not null,
    [CardType] nvarchar(100) not null,
    [CardName] nvarchar(256) not null,
    [CardNumber] nvarchar(100) not null,
    [PaidDate] datetime not null,
    [Deleted] bit not null,
    [CreatedDate] datetime not null,
    [Note] nvarchar(1024) not null,
    [OrderMessage] nvarchar(1024) not null,
    [ShippingExpense] decimal(18,4) not null,
    [CommissionCharge] decimal(18,4) not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_OrderItem' 
Create Table [dbo].[Nt_OrderItem] (
    [OrderItemGuid] uniqueidentifier not null,
    [OrderId] int not null,
    [GoodsId] int not null,
    [Quantity] int not null,
    [DiscountAmount] decimal(18,4) not null,
    [Adjustment] decimal(18,4) not null,
    [Price] decimal(18,4) not null,
    [SubTotalDiscount] decimal(18,4) not null,
    [SubTotal] decimal(18,4) not null,
    [SubMoneyforshipping] decimal(18,4) not null,
    [SubMoneyforpayment] decimal(18,4) not null,
    [SubTotalPoints] decimal(18,4) not null,
    [AttributesXml] nvarchar(1024) not null,
    [Note] nvarchar(1024) not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_OrderNote' 
Create Table [dbo].[Nt_OrderNote] (
    [OrderId] int not null,
    [Note] nvarchar(max) not null,
    [DisplayToCustomer] bit not null,
    [CreatedDate] datetime not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_PaymentMethod' 
Create Table [dbo].[Nt_PaymentMethod] (
    [Name] nvarchar(1024) not null,
    [Description] nvarchar(1024) not null,
    [DisplayOrder] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Permission' 
Create Table [dbo].[Nt_Permission] (
    [IsCategory] bit not null,
    [FatherId] int not null,
    [Name] nvarchar(1024) not null,
    [EnglishName] nvarchar(1024) not null,
    [SystemName] nvarchar(1024) not null,
    [Src] nvarchar(1024) not null,
    [Display] bit not null,
    [DisplayOrder] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Permission_UserLevel_Mapping' 
Create Table [dbo].[Nt_Permission_UserLevel_Mapping] (
    [PermissionId] int not null,
    [UserLevelId] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Picture' 
Create Table [dbo].[Nt_Picture] (
    [DisplayOrder] int not null,
    [Alt] nvarchar(1024) not null,
    [Title] nvarchar(1024) not null,
    [Description] nvarchar(1024) not null,
    [Display] bit not null,
    [Src] nvarchar(1024) not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Product' 
Create Table [dbo].[Nt_Product] (
    [Name] nvarchar(512) not null,
    [ShortDescription] nvarchar(max) not null,
    [FullDescription] nvarchar(max) not null,
    [Rating] int not null,
    [SetTop] bit not null,
    [Recommended] bit not null,
    [IsNew] bit not null,
    [SeoTitle] nvarchar(512) not null,
    [SeoKeywords] nvarchar(1024) not null,
    [SeoDescription] nvarchar(1024) not null,
    [UpdatedDate] datetime not null,
    [CreatedDate] datetime not null,
    [Display] bit not null,
    [DisplayOrder] int not null,
    [ProductCategoryId] int not null,
    [UserId] int not null,
    [Thumb] nvarchar(1024) not null,
    [PictureUrl] nvarchar(1024) not null,
    [FileName] nvarchar(1024) not null,
    [FileUrl] nvarchar(1024) not null,
    [FileSize] nvarchar(1024) not null,
    [Guid] uniqueidentifier not null,
    [AreaId] int not null,
    [Day] int not null,
    [F0] nvarchar(256) not null,
    [F1] nvarchar(256) not null,
    [F2] nvarchar(256) not null,
    [F3] nvarchar(256) not null,
    [F4] nvarchar(256) not null,
    [F5] nvarchar(256) not null,
    [F6] nvarchar(256) not null,
    [F7] nvarchar(256) not null,
    [F8] nvarchar(256) not null,
    [F9] nvarchar(256) not null,
    [F10] nvarchar(256) not null,
    [F11] nvarchar(256) not null,
    [F12] nvarchar(256) not null,
    [F13] nvarchar(256) not null,
    [F14] nvarchar(256) not null,
    [F15] nvarchar(256) not null,
    [Downloadable] bit not null,
    [LanguageId] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_ProductCategory' 
Create Table [dbo].[Nt_ProductCategory] (
    [LanguageId] int not null,
    [SeoTitle] nvarchar(512) not null,
    [SeoKeywords] nvarchar(1024) not null,
    [SeoDescription] nvarchar(1024) not null,
    [PictureUrl] nvarchar(256) not null,
    [Description] nvarchar(max) not null,
    [ListTemplate] nvarchar(256) not null,
    [DetailTemplate] nvarchar(256) not null,
    [Depth] int not null,
    [Parent] int not null,
    [Crumbs] nvarchar(512) not null,
    [Name] nvarchar(256) not null,
    [Display] bit not null,
    [DisplayOrder] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Product_Picture_Mapping' 
Create Table [dbo].[Nt_Product_Picture_Mapping] (
    [ProductId] int not null,
    [PictureId] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_ShippingMethod' 
Create Table [dbo].[Nt_ShippingMethod] (
    [Name] nvarchar(1024) not null,
    [Description] nvarchar(1024) not null,
    [DisplayOrder] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_ShoppingCartItem' 
Create Table [dbo].[Nt_ShoppingCartItem] (
    [CustomerId] int not null,
    [GoodsId] int not null,
    [Quantity] int not null,
    [AttributesXml] nvarchar(1024) not null,
    [CreatedDate] datetime not null,
    [UpdatedDate] datetime not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_SinglePage' 
Create Table [dbo].[Nt_SinglePage] (
    [Title] nvarchar(256) not null,
    [Short] nvarchar(1024) not null,
    [Body] nvarchar(max) not null,
    [FirstPicture] nvarchar(100) not null,
    [SeoTitle] nvarchar(1024) not null,
    [SeoKeywords] nvarchar(1024) not null,
    [SeoDescription] nvarchar(1024) not null,
    [ListTemplate] nvarchar(1024) not null,
    [DetailTemplate] nvarchar(1024) not null,
    [CreatedDate] datetime not null,
    [UpdatedDate] datetime not null,
    [Guid] uniqueidentifier not null,
    [LanguageId] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_Slider' 
Create Table [dbo].[Nt_Slider] (
    [PictureUrl] nvarchar(256) not null,
    [Url] nvarchar(256) not null,
    [Title] nvarchar(512) not null,
    [Display] bit not null,
    [DisplayOrder] int not null,
    [LanguageId] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_User' 
Create Table [dbo].[Nt_User] (
    [UserName] nvarchar(1024) not null,
    [Password] nvarchar(1024) not null,
    [Email] nvarchar(1024) not null,
    [UserLevelId] int not null,
    [LastLoginIp] nvarchar(1024) not null,
    [LoginTimes] int not null,
    [LastLoginDate] datetime not null,
    [CreatedDate] datetime not null,
    [CreatedUserId] int not null,
    [Active] bit not null,
    [Deleted] bit not null,
    [Profile] nvarchar(1024) not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_UserLevel' 
Create Table [dbo].[Nt_UserLevel] (
    [Name] nvarchar(1024) not null,
    [IsAdmin] bit not null,
    [Active] bit not null,
    [Note] nvarchar(1024) not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO
--Creating Table 'Nt_WebsiteLinkItem' 
Create Table [dbo].[Nt_WebsiteLinkItem] (
    [Word] nvarchar(1024) not null,
    [Url] nvarchar(1024) not null,
    [Applied] bit not null,
    [LanguageId] int not null,
    [Id] int primary key IDENTITY(1,1) not null,
);
GO


--Create All Foreign Key...
--Creating Foreign Key 'FK_Nt_Article_Nt_Article_Class' 
ALTER TABLE [dbo].[Nt_Article]
ADD CONSTRAINT [FK_Nt_Article_Nt_Article_Class]
FOREIGN KEY ([ArticleClassId]) REFERENCES [dbo].[Nt_Article_Class]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Article_Nt_Language' 
ALTER TABLE [dbo].[Nt_Article]
ADD CONSTRAINT [FK_Nt_Article_Nt_Language]
FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Banner_Nt_Language' 
ALTER TABLE [dbo].[Nt_Banner]
ADD CONSTRAINT [FK_Nt_Banner_Nt_Language]
FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Brand_Nt_Language' 
ALTER TABLE [dbo].[Nt_Brand]
ADD CONSTRAINT [FK_Nt_Brand_Nt_Language]
FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Customer_Nt_CustomerRole' 
ALTER TABLE [dbo].[Nt_Customer]
ADD CONSTRAINT [FK_Nt_Customer_Nt_CustomerRole]
FOREIGN KEY ([CustomerRoleId]) REFERENCES [dbo].[Nt_CustomerRole]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Customer_Consignee_Nt_Customer' 
ALTER TABLE [dbo].[Nt_Customer_Consignee]
ADD CONSTRAINT [FK_Nt_Customer_Consignee_Nt_Customer]
FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Nt_Customer]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Customer_Message_Nt_Customer' 
ALTER TABLE [dbo].[Nt_Customer_Message]
ADD CONSTRAINT [FK_Nt_Customer_Message_Nt_Customer]
FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Nt_Customer]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Customer_Wishlist_Nt_Customer' 
ALTER TABLE [dbo].[Nt_Customer_Wishlist]
ADD CONSTRAINT [FK_Nt_Customer_Wishlist_Nt_Customer]
FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Nt_Customer]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Customer_Wishlist_Nt_Goods' 
ALTER TABLE [dbo].[Nt_Customer_Wishlist]
ADD CONSTRAINT [FK_Nt_Customer_Wishlist_Nt_Goods]
FOREIGN KEY ([GoodsId]) REFERENCES [dbo].[Nt_Goods]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_FriendLink_Nt_Language' 
ALTER TABLE [dbo].[Nt_FriendLink]
ADD CONSTRAINT [FK_Nt_FriendLink_Nt_Language]
FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Goods_Nt_Goods_Class' 
ALTER TABLE [dbo].[Nt_Goods]
ADD CONSTRAINT [FK_Nt_Goods_Nt_Goods_Class]
FOREIGN KEY ([GoodsClassId]) REFERENCES [dbo].[Nt_Goods_Class]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Goods_Nt_Language' 
ALTER TABLE [dbo].[Nt_Goods]
ADD CONSTRAINT [FK_Nt_Goods_Nt_Language]
FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Goods_Parameter_Nt_Goods_ParameterGroup' 
ALTER TABLE [dbo].[Nt_Goods_Parameter]
ADD CONSTRAINT [FK_Nt_Goods_Parameter_Nt_Goods_ParameterGroup]
FOREIGN KEY ([GoodsParameterGroupId]) REFERENCES [dbo].[Nt_Goods_ParameterGroup]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Goods_Parameter_Nt_Goods' 
ALTER TABLE [dbo].[Nt_Goods_Parameter]
ADD CONSTRAINT [FK_Nt_Goods_Parameter_Nt_Goods]
FOREIGN KEY ([GoodsId]) REFERENCES [dbo].[Nt_Goods]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Goods_Ask_Nt_Goods' 
ALTER TABLE [dbo].[Nt_Goods_Ask]
ADD CONSTRAINT [FK_Nt_Goods_Ask_Nt_Goods]
FOREIGN KEY ([GoodsId]) REFERENCES [dbo].[Nt_Goods]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Goods_Ask_Nt_Customer' 
ALTER TABLE [dbo].[Nt_Goods_Ask]
ADD CONSTRAINT [FK_Nt_Goods_Ask_Nt_Customer]
FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Nt_Customer]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Goods_Attribute_Mapping_Nt_GoodsAttribute' 
ALTER TABLE [dbo].[Nt_Goods_Attribute_Mapping]
ADD CONSTRAINT [FK_Nt_Goods_Attribute_Mapping_Nt_GoodsAttribute]
FOREIGN KEY ([GoodsAttributeId]) REFERENCES [dbo].[Nt_GoodsAttribute]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Goods_Attribute_Mapping_Nt_Goods' 
ALTER TABLE [dbo].[Nt_Goods_Attribute_Mapping]
ADD CONSTRAINT [FK_Nt_Goods_Attribute_Mapping_Nt_Goods]
FOREIGN KEY ([GoodsId]) REFERENCES [dbo].[Nt_Goods]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Goods_Promotion_Mapping_Nt_Goods' 
ALTER TABLE [dbo].[Nt_Goods_Promotion_Mapping]
ADD CONSTRAINT [FK_Nt_Goods_Promotion_Mapping_Nt_Goods]
FOREIGN KEY ([GoodsId]) REFERENCES [dbo].[Nt_Goods]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Goods_Promotion_Mapping_Nt_Goods_Promotion' 
ALTER TABLE [dbo].[Nt_Goods_Promotion_Mapping]
ADD CONSTRAINT [FK_Nt_Goods_Promotion_Mapping_Nt_Goods_Promotion]
FOREIGN KEY ([PromotionId]) REFERENCES [dbo].[Nt_Goods_Promotion]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Goods_VariantAttributeValue_Nt_Goods_Attribute_Mapping' 
ALTER TABLE [dbo].[Nt_Goods_VariantAttributeValue]
ADD CONSTRAINT [FK_Nt_Goods_VariantAttributeValue_Nt_Goods_Attribute_Mapping]
FOREIGN KEY ([GoodsVariantAttributeId]) REFERENCES [dbo].[Nt_Goods_Attribute_Mapping]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Goods_Binding_Nt_Goods' 
ALTER TABLE [dbo].[Nt_Goods_Binding]
ADD CONSTRAINT [FK_Nt_Goods_Binding_Nt_Goods]
FOREIGN KEY ([GoodsId]) REFERENCES [dbo].[Nt_Goods]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Goods_Picture_Mapping_Nt_Goods' 
ALTER TABLE [dbo].[Nt_Goods_Picture_Mapping]
ADD CONSTRAINT [FK_Nt_Goods_Picture_Mapping_Nt_Goods]
FOREIGN KEY ([GoodsId]) REFERENCES [dbo].[Nt_Goods]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Goods_Picture_Mapping_Nt_Picture' 
ALTER TABLE [dbo].[Nt_Goods_Picture_Mapping]
ADD CONSTRAINT [FK_Nt_Goods_Picture_Mapping_Nt_Picture]
FOREIGN KEY ([PictureId]) REFERENCES [dbo].[Nt_Picture]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Goods_Review_Nt_Customer' 
ALTER TABLE [dbo].[Nt_Goods_Review]
ADD CONSTRAINT [FK_Nt_Goods_Review_Nt_Customer]
FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Nt_Customer]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Goods_Review_Nt_Goods' 
ALTER TABLE [dbo].[Nt_Goods_Review]
ADD CONSTRAINT [FK_Nt_Goods_Review_Nt_Goods]
FOREIGN KEY ([GoodsId]) REFERENCES [dbo].[Nt_Goods]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Goods_Tag_Nt_Language' 
ALTER TABLE [dbo].[Nt_Goods_Tag]
ADD CONSTRAINT [FK_Nt_Goods_Tag_Nt_Language]
FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_GuestBook_Nt_Language' 
ALTER TABLE [dbo].[Nt_GuestBook]
ADD CONSTRAINT [FK_Nt_GuestBook_Nt_Language]
FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_GuestBookReply_Nt_GuestBook' 
ALTER TABLE [dbo].[Nt_GuestBookReply]
ADD CONSTRAINT [FK_Nt_GuestBookReply_Nt_GuestBook]
FOREIGN KEY ([GuestBookId]) REFERENCES [dbo].[Nt_GuestBook]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Measure_Nt_Language' 
ALTER TABLE [dbo].[Nt_Measure]
ADD CONSTRAINT [FK_Nt_Measure_Nt_Language]
FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Order_Nt_Customer' 
ALTER TABLE [dbo].[Nt_Order]
ADD CONSTRAINT [FK_Nt_Order_Nt_Customer]
FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Nt_Customer]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_OrderItem_Nt_Order' 
ALTER TABLE [dbo].[Nt_OrderItem]
ADD CONSTRAINT [FK_Nt_OrderItem_Nt_Order]
FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Nt_Order]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_OrderNote_Nt_Order' 
ALTER TABLE [dbo].[Nt_OrderNote]
ADD CONSTRAINT [FK_Nt_OrderNote_Nt_Order]
FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Nt_Order]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Permission_UserLevel_Mapping_Nt_Permission' 
ALTER TABLE [dbo].[Nt_Permission_UserLevel_Mapping]
ADD CONSTRAINT [FK_Nt_Permission_UserLevel_Mapping_Nt_Permission]
FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Nt_Permission]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Permission_UserLevel_Mapping_Nt_UserLevel' 
ALTER TABLE [dbo].[Nt_Permission_UserLevel_Mapping]
ADD CONSTRAINT [FK_Nt_Permission_UserLevel_Mapping_Nt_UserLevel]
FOREIGN KEY ([UserLevelId]) REFERENCES [dbo].[Nt_UserLevel]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Product_Nt_ProductCategory' 
ALTER TABLE [dbo].[Nt_Product]
ADD CONSTRAINT [FK_Nt_Product_Nt_ProductCategory]
FOREIGN KEY ([ProductCategoryId]) REFERENCES [dbo].[Nt_ProductCategory]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Product_Nt_Language' 
ALTER TABLE [dbo].[Nt_Product]
ADD CONSTRAINT [FK_Nt_Product_Nt_Language]
FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Product_Picture_Mapping_Nt_Product' 
ALTER TABLE [dbo].[Nt_Product_Picture_Mapping]
ADD CONSTRAINT [FK_Nt_Product_Picture_Mapping_Nt_Product]
FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Nt_Product]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Product_Picture_Mapping_Nt_Picture' 
ALTER TABLE [dbo].[Nt_Product_Picture_Mapping]
ADD CONSTRAINT [FK_Nt_Product_Picture_Mapping_Nt_Picture]
FOREIGN KEY ([PictureId]) REFERENCES [dbo].[Nt_Picture]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_ShoppingCartItem_Nt_Customer' 
ALTER TABLE [dbo].[Nt_ShoppingCartItem]
ADD CONSTRAINT [FK_Nt_ShoppingCartItem_Nt_Customer]
FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Nt_Customer]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_ShoppingCartItem_Nt_Goods' 
ALTER TABLE [dbo].[Nt_ShoppingCartItem]
ADD CONSTRAINT [FK_Nt_ShoppingCartItem_Nt_Goods]
FOREIGN KEY ([GoodsId]) REFERENCES [dbo].[Nt_Goods]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_SinglePage_Nt_Language' 
ALTER TABLE [dbo].[Nt_SinglePage]
ADD CONSTRAINT [FK_Nt_SinglePage_Nt_Language]
FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_Slider_Nt_Language' 
ALTER TABLE [dbo].[Nt_Slider]
ADD CONSTRAINT [FK_Nt_Slider_Nt_Language]
FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_User_Nt_UserLevel' 
ALTER TABLE [dbo].[Nt_User]
ADD CONSTRAINT [FK_Nt_User_Nt_UserLevel]
FOREIGN KEY ([UserLevelId]) REFERENCES [dbo].[Nt_UserLevel]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO
--Creating Foreign Key 'FK_Nt_WebsiteLinkItem_Nt_Language' 
ALTER TABLE [dbo].[Nt_WebsiteLinkItem]
ADD CONSTRAINT [FK_Nt_WebsiteLinkItem_Nt_Language]
FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION;
GO


--Create All Views
--Creating View 'View_Article' 
Create View [dbo].[View_Article]
As
 Select *,(Select max(ID) From Nt_Article) As MaxID From Nt_Article  Left Join (Select Id As TempID,Name As ClassName,Crumbs As ClassCrumbs,ListTemplate,DetailTemplate From Nt_Article_Class) As T  On Nt_Article.ArticleClassId=T.TempID
GO
--Creating View 'View_ArticleClass' 
Create View [dbo].[View_ArticleClass]
As
 Select *,(Select max(ID) From Nt_Article_Class) As MaxID,Case [Parent]
 When 0 Then N'Root'
 Else  (Select Top 1 [Name] From Nt_Article_Class As T1 Where ID=T0.Parent) End
 As ParentName, Case [Parent]
 When 0 Then ListTemplate
 Else  (Select Top 1 [ListTemplate] From Nt_Article_Class As T1 Where ID=T0.Parent) End
 As DefaultListTemplate, Case [Parent]
 When 0 Then DetailTemplate
 Else  (Select Top 1 [DetailTemplate] From Nt_Article_Class As T1 Where ID=T0.Parent) End
 As DefaultDetailTemplate, (Select Count(0) From Nt_Article Where Display=1 And ArticleClassId=T0.Id) As Total  From Nt_Article_Class As T0
GO
--Creating View 'View_Banner' 
Create View [dbo].[View_Banner]
As
 Select *, (Select max(ID) From Nt_Banner) As MaxID From Nt_Banner As T0
GO
--Creating View 'View_Customer_Wishlist' 
Create View [dbo].[View_Customer_Wishlist]
As
Select *,0 As MaxID, (Select Name From Nt_Goods Where ID=T1.GoodsId) As GoodsName, (Select Name From Nt_Customer Where ID=T1.CustomerId) As CustomerName From Nt_Customer_Wishlist As T1
GO
--Creating View 'View_FriendLink' 
Create View [dbo].[View_FriendLink]
As
 Select *, (Select max(ID) From Nt_FriendLink) As MaxID From Nt_FriendLink As T0
GO
--Creating View 'View_Goods' 
Create View [dbo].[View_Goods]
As
 Select *,(Select max(ID) From Nt_Goods) As MaxID From ( Select *,cast(DiscountId as bit) As UseDiscount, (select count(0) from Nt_goods_review where goodsid=T0.Id) As Reviews, (select [name] from Nt_brand where Id=T0.BrandId) as BrandName, (select [pictureurl] from Nt_brand where Id=T0.BrandId) as BrandLogo From Nt_Goods As T0 Left Join  (Select Id As TempID1, Name As ClassName,Crumbs As ClassCrumbs,ListTemplate,DetailTemplate From Nt_Goods_Class) As T1 ON T0.GoodsClassId=T1.TempID1) As T0T1 Left Join  (Select Id As TempID2, Name As DiscountName,UsePercentage,DiscountPercentage,DiscountAmount From Nt_Discount) As T2 ON T0T1.DiscountId=T2.TempID2
GO
--Creating View 'View_GoodsAttribute' 
Create View [dbo].[View_GoodsAttribute]
As
 Select *, (Select max(ID) From Nt_Goods_Attribute_Mapping) As MaxID  From Nt_Goods_Attribute_Mapping As T0, (Select Id As TempID,Name,Description From Nt_GoodsAttribute) As T1 Where T0.GoodsAttributeId=T1.TempID
GO
--Creating View 'View_GoodsClass' 
Create View [dbo].[View_GoodsClass]
As
 Select *, (Select max(ID) From Nt_Goods_Class) As MaxID, Case [Parent]
 When 0 Then N'Root'
 Else  (Select Top 1 [Name] From Nt_Goods_Class As T1 Where ID=T0.Parent) End
 As ParentName, Case [Parent]
 When 0 Then ListTemplate
 Else  (Select Top 1 [ListTemplate] From Nt_Goods_Class As T1 Where ID=T0.Parent) End
 As DefaultListTemplate, Case [Parent]
 When 0 Then DetailTemplate
 Else  (Select Top 1 [DetailTemplate] From Nt_Goods_Class As T1 Where ID=T0.Parent) End
 As DefaultDetailTemplate,  (Select Count(0) From Nt_Goods Where Display=1 And GoodsClassId=T0.Id) As Total  From Nt_Goods_Class As T0
GO
--Creating View 'View_GoodsParameter' 
Create View [dbo].[View_GoodsParameter]
As
 Select *, (Select max(ID) From Nt_Goods_Parameter) As MaxID From Nt_Goods_Parameter As T0  Left Join  (Select Id  As TempID,GroupName As ParamGroupName From Nt_Goods_ParameterGroup) As T1 On T0.GoodsParameterGroupId=T1.TempID
GO
--Creating View 'View_GoodsPicture' 
Create View [dbo].[View_GoodsPicture]
As
 Select  (Select max(ID) From Nt_Picture) As MaxID, DisplayOrder,Alt,Title,Description,Display,Src,Id,GoodsId From  Nt_Picture  As T1, (Select GoodsId,PictureId From Nt_Goods_Picture_Mapping) As T0  Where T0.PictureId=T1.Id
GO
--Creating View 'View_Customer' 
Create View [dbo].[View_Customer]
As
 Select *,(Select max(ID) From Nt_Customer) As MaxID, (select count(0) from Nt_shoppingcartitem where CustomerId=t0.id) as TotalOfShoppingCartItem, (select count(0) from Nt_goods_review where CustomerId=t0.id) as TotalOfReviews, (select count(0) from Nt_Customer_Wishlist where CustomerId=t0.id) as TotalOfWishlist, (select count(0) from Nt_Goods_Ask where CustomerId=t0.id) as TotalOfAsk, (select count(0) from Nt_Customer_Message where CustomerId=t0.id) as TotalOfMessage, (select count(0) from Nt_Customer_Consignee where CustomerId=t0.id) as TotalOfConsignee From Nt_Customer as T0 Left Join  (Select Id As TempID,Name As RoleName,MeetPoints As RoleMeetPoints From Nt_CustomerRole) As T  On t0.CustomerRoleId=T.TempID
GO
--Creating View 'View_Goods_Ask' 
Create View [dbo].[View_Goods_Ask]
As
Select *,0 As MaxID, (Select Name From Nt_Goods Where ID=T1.GoodsId) As GoodsName, (Select Name From Nt_Customer Where ID=T1.CustomerId) As CustomerName From Nt_Goods_Ask As T1
GO
--Creating View 'View_Goods_Binding' 
Create View [dbo].[View_Goods_Binding]
As
Select *,0 As MaxID From Nt_Goods_Binding As T0 Left Join (Select Id As TempID,Name As BingGoodsName From Nt_Goods) As T1 On T0.BindingId=T1.TempID
GO
--Creating View 'View_Goods_Review' 
Create View [dbo].[View_Goods_Review]
As
Select *,0 As MaxID, (Select Name From Nt_Goods Where ID=T1.GoodsId) As GoodsName, (Select Name From Nt_Customer Where ID=T1.CustomerId) As CustomerName From Nt_Goods_Review As T1
GO
--Creating View 'View_GuestBook' 
Create View [dbo].[View_GuestBook]
As
 Select *,0 As MaxID, (Select Count(Id) From Nt_GuestBookReply Where GuestBookId=T0.Id) As RepliedCount, cast((Select Count(Id) From Nt_GuestBookReply Where GuestBookId=T0.Id) as bit) As Replied From Nt_GuestBook As T0
GO
--Creating View 'View_GuestBookReply' 
Create View [dbo].[View_GuestBookReply]
As
 Select *, 0 As MaxID, (Select Title From Nt_GuestBook Where Id=T0.GuestBookId) As GuestBookTitle From Nt_GuestBookReply As T0
GO
--Creating View 'View_Language' 
Create View [dbo].[View_Language]
As
 Select *, (Select max(ID) From Nt_Language) As MaxID From Nt_Language As T0
GO
--Creating View 'View_Log' 
Create View [dbo].[View_Log]
As
 Select *, 0 As MaxID, (Select [UserName] From Nt_User Where ID=T0.UserID) As UserName From Nt_Log As T0
GO
--Creating View 'View_Navigation' 
Create View [dbo].[View_Navigation]
As
 Select *, (Select max(ID) From Nt_Navigation) As MaxID, Case [Parent]
 When 0 Then N'Root'
 Else  (Select Top 1 [Name] From Nt_Navigation As T1 Where ID=T0.Parent) End
 As ParentName From Nt_Navigation As T0
GO
--Creating View 'View_Order' 
Create View [dbo].[View_Order]
As
 Select *, (Select max(ID) From Nt_Order) As MaxID, (Select Top 1 Name From Nt_PaymentMethod Where Id=PaymentMethodId) As PaymentMethod, (Select Top 1 Name From Nt_ShippingMethod Where Id=ShippingMethodId) As ShippingMethod From Nt_Order As T0 Left Join (Select Id As TempID,[Name],[Phone],[Mobile],[Email],[Address],[Zip] From Nt_Customer_Consignee) As T1 On T0.CustomerConsigneeId=T1.TempID
GO
--Creating View 'View_OrderItem' 
Create View [dbo].[View_OrderItem]
As
 Select *, 0 As MaxID From Nt_OrderItem As T0 Left Join (Select Id As TempId,GoodsGuid, Name As GoodsName,Price As GoodsPrice,Weight As GoodsWeight,  Height As GoodsHeight,Width As GoodsWidth,Length As GoodsLength From Nt_Goods) As T1 On T0.GoodsId=T1.TempId
GO
--Creating View 'View_Permission' 
Create View [dbo].[View_Permission]
As
 Select *, (Select max(ID) From Nt_Permission) As MaxID,  (case  when T0.IsCategory=1 then T0.Src 
  else (Select Top 1 Src From Nt_Permission Where ID=T0.FatherId) end) As Ico From Nt_Permission As T0
GO
--Creating View 'View_Product' 
Create View [dbo].[View_Product]
As
 Select *,(Select max(ID) From Nt_Product) As MaxID, (Select [Name] From Nt_Area Where Id=Nt_Product.AreaId) As AreaName,  (Select [Date] From Nt_Day Where Id=Nt_Product.Day) As DayDate,  (Select [Title] From Nt_Day Where Id=Nt_Product.Day) As DayTitle From Nt_Product  Left Join (Select Id As TempID,Name As CategoryName,Crumbs As CategoryCrumbs,ListTemplate,DetailTemplate From Nt_ProductCategory) As T  On Nt_Product.ProductCategoryId=T.TempID
GO
--Creating View 'View_ProductCategory' 
Create View [dbo].[View_ProductCategory]
As
 Select *,(Select max(ID) From Nt_ProductCategory) As MaxID,Case [Parent]
 When 0 Then N'Root'
 Else  (Select Top 1 [Name] From Nt_ProductCategory As T1 Where ID=T0.Parent) End
 As ParentName, Case [Parent]
 When 0 Then ListTemplate
 Else  (Select Top 1 [ListTemplate] From Nt_ProductCategory As T1 Where ID=T0.Parent) End
 As DefaultListTemplate, Case [Parent]
 When 0 Then DetailTemplate
 Else  (Select Top 1 [DetailTemplate] From Nt_ProductCategory As T1 Where ID=T0.Parent) End
 As DefaultDetailTemplate,  (Select Count(0) From Nt_Product Where Display=1 And ProductCategoryId=T0.Id) As Total  From Nt_ProductCategory As T0
GO
--Creating View 'View_ProductPicture' 
Create View [dbo].[View_ProductPicture]
As
 Select  (Select max(ID) From Nt_Picture) As MaxID, DisplayOrder,Alt,Title,Description,Display,Src,Id,ProductId From  Nt_Picture  As T1, (Select ProductId,PictureId From Nt_Product_Picture_Mapping) As T0  Where T0.PictureId=T1.Id
GO
--Creating View 'View_Slider' 
Create View [dbo].[View_Slider]
As
 Select *, (Select max(ID) From Nt_Slider) As MaxID From Nt_Slider As T0
GO
--Creating View 'View_User' 
Create View [dbo].[View_User]
As
 Select *, (Select max(ID) From Nt_User) As MaxID From Nt_User As T0  Left Join  (Select Id  As TempID,Name As LevelName,IsAdmin,Active As LevelActive From Nt_UserLevel) As T1 On T0.UserLevelId=T1.TempID
GO
--Creating View 'View_User_Permission' 
Create View [dbo].[View_User_Permission]
As
 Select * From  (Select PermissionId,UserLevelId From Nt_Permission_UserLevel_Mapping) As T0 , (Select *,0 As MaxID From Nt_Permission) As T1 Where T0.PermissionId=T1.Id
GO
--Creating View 'View_GoodsVariantAttributeValue' 
Create View [dbo].[View_GoodsVariantAttributeValue]
As
 Select *, (Select max(ID) From Nt_Goods_VariantAttributeValue) As MaxID From Nt_Goods_VariantAttributeValue As T0 Left Join (Select Id As TempID, GoodsId As AssociatedGoodsId,ControlType As AttributeValueTypeId,Name As AttributeName From View_GoodsAttribute) As T1 On T0.GoodsVariantAttributeId=T1.TempID
GO
--Creating View 'View_ShoppingCart' 
Create View [dbo].[View_ShoppingCart]
As
 Select *, (Select max(ID) From Nt_ShoppingCartItem) As MaxID,  (select [name] from Nt_customer where id=t0.customerid) as CustomerName From Nt_ShoppingCartItem As T0 Left Join (Select Id As TempID, Name As GoodsName, GoodsGuid,PictureUrl,ClassName,Price As UnitPrice,EnableVipPrice,VipPrice As UnitVipPrice,SpecialPrice As UnitSpecialPrice, Points,DiscountName,UsePercentage,DiscountPercentage,DiscountAmount, EnableSpecialPrice,SpecialPriceStartDate,SpecialPriceEndDate, [Weight] As ItemWeight, Cost As OriginalGoodsCost,Measure,UseDiscount, Height As ItemHeight, Width As ItemWidth, Length As ItemLength From View_Goods) As T1 On T0.GoodsId=T1.TempID
GO
