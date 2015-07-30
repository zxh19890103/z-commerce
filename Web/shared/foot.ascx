<%@ Control Language="C#" ClassName="foot" Inherits="Nt.Framework.BaseAscx" %>

<!----footer begin--->
<div class="footer">
    <div class="container">
        <div class="col-md-3 footer-logo">
            <a href="/index.aspx">
                <img src="/content/images/flogo.png" title="brand-logo" alt="" /></a>
        </div>
        <div class="col-md-7 footer-links">
            <ul class="unstyled-list list-inline">
                <li><a href="/faq.aspx">Faq</a> <span></span></li>
                <li><a href="/terms.aspx">条款/条件</a> <span></span></li>
                <li><a href="/payments.aspx">安全支付</a> <span></span></li>
                <li><a href="/shippings.aspx">配送</a> <span></span></li>
                <li><a href="/contact.aspx">联系我们</a> </li>
            </ul>
            <div class="clearfix"></div>
        </div>
        <div class="col-md-2 footer-social">
            <ul class="unstyled-list list-inline">
                <%--<li><a class="pin" href="#"><span></span></a></li>
                <li><a class="twitter" href="#"><span></span></a></li>
                <li><a class="facebook" href="#"><span></span></a></li>      --%>
            </ul>
            <div class="clearfix"></div>
        </div>
        <div class="clearfix"></div>
    </div>
</div>
<div class="clearfix"></div>
<!---//footer--->
<!---copy-right--->
<div class="copy-right">
    <div class="container">
        <p>Copyright &copy; 2014.<a target="_blank" href="http://www.naite.com.cn">Naite Power</a> All rights reserved.</p>
        <script type="text/javascript">
            $(document).ready(function () {
                /*
                var defaults = {
                    containerID: 'toTop', // fading element id
                    containerHoverID: 'toTopHover', // fading element hover id
                    scrollSpeed: 1200,
                    easingType: 'linear' 
                };
                */

                $().UItoTop({ easingType: 'easeOutQuart' });

            });
        </script>
        <a href="#" id="toTop" style="display: block;"><span id="toTopHover" style="opacity: 1;"></span></a>
    </div>
</div>
<!--//copy-right--->
