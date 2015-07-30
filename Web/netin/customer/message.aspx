<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/netin/layout.master" CodeFile="message.aspx.cs" Inherits="netin_customer_message" %>


<asp:Content ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var tMgr = new nt.tableMgr({
            title: '会员消息',
            ajax2page: true
        });
        tMgr.init();

        /*发送消息*/
        function sendmsg(msgid) {
            var customers=<%=NtUtility.GetJsObjectArrayFromList(DB.GetDropdownlist<Customer>("Name","Id","Active=1"))%>;
            nt.openMutiSelectorWin('选择会员',customers,'',function(arg){
                var ids=[];
                $('input:checked',arg).each(function(i,n){
                    ids.push($(this).val());
                });
                if(ids.length==0){error('您未选择任何会员!');return;}
                nt.ajax(
                    {
                        action:'SendMsg',
                        data:{msgid:msgid,ids:ids.join()},
                        success:function(j){
                            if(j.error)error(j.message);
                            else success(j.message);
                        }
                    }
                    );

            },600);
        }

    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <div class="list" id="list">
        <%List(); %>
    </div>

    <div class="html-content-wrap">
        <div class="html-content" id="tmEditor">
            <div class="html-content-top nt-drag-bar">
                <span class="html-content-title"></span><a href="javascript:;" role="close">x</a>
            </div>
            <div class="html-content-body">
                <form action="message.aspx" method="post" id="tmForm">
                    <table class="adminContent">
                        <tr>
                            <td class="adminTitle">:标题
                            </td>
                            <td class="adminData">
                                <input type="text" maxlength="50" class="input-long" name="Title" />
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">消息内容:
                            </td>
                            <td class="adminData">
                                <textarea cols="1" rows="2" class="textarea-normal" name="Body"></textarea>
                            </td>
                        </tr>
                    </table>
                    <input type="hidden" name="CreatedDate" value="<%=Model.CreatedDate %>" />
                    <input type="hidden" name="Id" value="0" />
                </form>
            </div>
            <div class="html-content-footer">
                <a href="javascript:;" class="a-button" role="post">保存</a> <a href="javascript:;"
                    class="a-button" role="close">关闭</a>
            </div>
        </div>
    </div>

</asp:Content>
