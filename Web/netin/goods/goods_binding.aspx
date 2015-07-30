<%@ Page Language="C#" AutoEventWireup="false" CodeFile="goods_binding.aspx.cs" Inherits="netin_goods_goods_binding" %>

<%@ Register Src="~/netin/shared/head.ascx" TagPrefix="uc1" TagName="head" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>添加相关商品</title>
    <uc1:head runat="server" ID="head" />
    <script type="text/javascript">
        function ok() {
            var bindings = getSelectedIds();
            var data = {};
            data.goods = <%=goodsId%>;
            data.bindings = bindings.join();
            nt.ajax({
                data:data,
                action:'SaveBindings',
                success:function(json){
                    if(json.error){
                        alert(json.message,function(){
                            window.close();
                        });
                    }else{
                        GetOpener().refreshGoodsBinding();
                        window.close();
                    }
                }
            });
        }
    </script>
</head>
<body>
    <div id="list" class="list">
        <%List(); %>
    </div>
    <div class="submit">
        <a href="javascript:;" class="a-button" onclick="ok();">确定</a>
        <a href="javascript:;" class="a-button" onclick="window.close();">关闭</a>
    </div>
</body>
</html>
