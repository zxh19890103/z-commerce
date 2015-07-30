<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>

<uc1:top runat="server" ID="top1" />
<script src="script/form.validate.js" type="text/javascript"></script>
<script type="text/javascript" src="http://api.map.baidu.com/api?key=&v=1.1&services=true"></script>
<script src="/netin/script/map.js" type="text/javascript"></script>

<div class="content">
    <!----contact---->
    <div class="contact-info">

        <div class="container">

            <div class="map" id="dituContent" style="width: 100%; height: 400px; margin: 10px auto; border: 1px solid #ddd;">
            </div>

            <div class="contact-grids">
                <div class="col_1_of_bottom span_1_of_first1">
                    <h5>地址</h5>
                    <ul class="list3">
                        <li>
                            <img src="/content/images/home.png" alt="" />
                            <div class="extra-wrap">
                                <p>
                                    中山区&nbsp;&nbsp;人民路&nbsp;锦联大厦1505室
                                </p>
                            </div>
                        </li>
                    </ul>
                </div>
                <div class="col_1_of_bottom span_1_of_first1">
                    <h5>电话</h5>
                    <ul class="list3">
                        <li>
                            <img src="/content/images/phone.png" alt="" />
                            <div class="extra-wrap">
                                <p><span>固话:</span>0411-82527801</p>
                            </div>
                            <img src="/content/images/fax.png" alt="" />
                            <div class="extra-wrap">
                                <p><span>传真:</span>0411-82527801</p>
                            </div>
                        </li>
                    </ul>
                </div>
                <div class="col_1_of_bottom span_1_of_first1">
                    <h5>邮箱</h5>
                    <ul class="list3">
                        <li>
                            <img src="/content/images/email.png" alt="" />
                            <div class="extra-wrap">
                                <p><span class="mail"><a href="mailto:zhangxinghai@naite.com.cm">zhangxinghai@naite.com.cm</a></span></p>
                            </div>
                        </li>
                    </ul>
                </div>
                <div class="clearfix"></div>
            </div>
            <form action="handlers/ajaxHandler.aspx" id="contactForm">
                <div class="contact-form">
                    <div class="contact-to">
                        <input type="text" class="text smart-input" value="姓名..." name="Name" />
                        <input type="text" class="text smart-input" value="邮箱..." name="Email" />
                        <input type="text" class="text smart-input" value="主题..." name="Title" />
                    </div>
                    <div class="text2">
                        <textarea rows="2" cols="1" class="smart-input" name="Body">内容..</textarea>
                    </div>
                    <%
                        if (GBSettings.EnableCheckCode)
                        {
                    %>
                    <div class="contact-checkcode">
                        <input type="text" class="text smart-input" value="验证码..." name="checkcode" />
                        <img title="checkcode" src="handlers/checkCodeGenerator.aspx" class="nt-checkcode" alt="load failed" />
                    </div>
                    <br />
                    <%} %>
                    <span>
                        <input type="submit" class="" value="发送" onclick="return send();" /></span>
                    <div class="clearfix"></div>
                </div>
            </form>
        </div>
    </div>
</div>
<script type="text/javascript">

    //121.655600  38.928100
    new nt.map({ x: 121.655600, y: 38.928100 });//地图

    registerSmartInput('.smart-input');

    //发送留言
    function send() {
        var form = document.getElementById('contactForm');
        if (!validateGuestBookForm(form))
            return;
        var data = {};
        data.Name = form.Name.value;
        data.Email = form.Email.value;
        data.Title = form.Title.value;
        data.Body = form.Body.value;
        data.checkcode = form.checkcode.value;
        nt.ajax({
            action: 'SubmitGuestBook',
            data: data,
            success: function (j) {
                if (j.error) alert(j.message);
                else {
                    alert(j.message);
                }
            }
        });
        return false;
    }
</script>
<uc1:foot runat="server" ID="foot" />
