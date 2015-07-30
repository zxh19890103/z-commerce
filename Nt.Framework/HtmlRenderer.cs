using Nt.BLL;
using Nt.Framework.Admin;
using Nt.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Nt.Framework
{
    public class HtmlRenderer
    {
        HttpResponse Response
        {
            get { return WebHelper.Response; }
        }

        /// <summary>
        /// 下拉选择框
        /// </summary>
        /// <param name="data">数据源</param>
        /// <param name="name">名</param>
        public void DropDownList(IEnumerable<NtListItem> data, string name)
        {
            DropDownList(data, name, string.Empty);
        }

        public void DropDownList(IEnumerable<NtListItem> data, string name, string onchange, int width)
        {
            if (data == null || data.Count() < 1) return;
            NtListItem selectedItem = data.FirstOrDefault(x => x.Selected);
            if (selectedItem == null)
            {
                selectedItem = data.FirstOrDefault();
                selectedItem.Selected = true;
            }

            #region old
            //Response.Write("<div class=\"drop-down-list\" id=\"" + name + "\">");
            //Response.Write("	<a href=\"javascript:;\" onclick=\"ddlExtends(this);\" class=\"drop-down-list-bar\">");
            //Response.Write("		<span>" + selectedItem.Text + "</span>");
            //Response.Write("		<i class=\"drop-button\"></i>");
            //Response.Write("	</a>");
            //Response.Write("	<ul class=\"drop-down-list-data\">");
            //foreach (var item in data)
            //{
            //    Response.Write("<li class=\"ddl-item");
            //    if (item.Selected)
            //        Response.Write(" ddl-current");
            //    Response.Write("\" data-value=\"");
            //    Response.Write(item.Value);
            //    Response.Write("\"><span>");
            //    Response.Write(item.Text);
            //    Response.Write("</span></li>");
            //}
            //Response.Write("	</ul>");
            //Response.Write("	<input type=\"hidden\" name=\"");
            //Response.Write(name);
            //Response.Write("\" value=\"" + selectedItem.Value + "\" />");
            //Response.Write("</div>");
            #endregion

            Response.Write("<select");
            if (!string.IsNullOrEmpty(onchange))
                Response.Write(" onchange=\"" + onchange + "\"");
            if (width > 0)
                Response.Write(" style=\"width:" + width + "px\"");
            Response.Write(" name=\"" + name + "\" id=\"" + name + "\">");
            foreach (var item in data)
            {
                Response.Write("<option value=\"" + item.Value + "\"");
                if (item.Selected)
                    Response.Write("selected=\"selected\"");
                Response.Write(">");
                Response.Write(item.Text);
                Response.Write("</option>");
            }
            Response.Write("</select>");
        }

        public void DropDownList(IEnumerable<NtListItem> data, string name, string onchange)
        {
            DropDownList(data, name, onchange, 0);
        }

        public void DropDownList(IEnumerable<NtListItem> data, string name, int width)
        {
            DropDownList(data, name, "", width);
        }

        public void DropDownList(IEnumerable<NtListItem> data, string name, bool begining, params NtListItem[] addItems)
        {
            int count = 0;
            if (data == null || (count = data.Count()) < 1)
                return;
            if (addItems != null)
            {
                int total = count + addItems.Length;
                NtListItem[] copy = new NtListItem[total];
                int startIndex = 0;
                startIndex = begining ? 0 : count;
                for (int j = 0; j < addItems.Length; j++)
                    copy[startIndex + j] = addItems[j];
                startIndex = begining ? addItems.Length : 0;
                for (int j = 0; j < count; j++)
                    copy[startIndex + j] = data.ElementAt(j);
                DropDownList(copy, name);
            }
            else
            {
                DropDownList(data, name);
            }
        }

        /// <summary>
        /// True/False复选框
        /// </summary>
        /// <param name="isChecked">初值</param>
        /// <param name="name">名</param>
        public void CheckBox(bool isChecked, string name)
        {
            Response.Write("<div class=\"on-off\" id=\"" + name + "\">");
            Response.Write("	<a href=\"javascript:;\" onclick=\"onOff(this);\" class=\"on-off-button");
            if (isChecked)
                Response.Write(" on");
            Response.Write("\">");
            Response.Write("</a>");
            Response.Write("	<input type=\"hidden\" nt-type=\"checkbox\" name=\"");
            Response.Write(name);
            Response.Write("\" value=\"");
            if (isChecked)
                Response.Write("true");
            else
                Response.Write("false");
            Response.Write("\" />");
            Response.Write("</div>");
        }

        /// <summary>
        /// True/False复选框
        /// </summary>
        /// <param name="isChecked">初值</param>
        /// <param name="name">名</param>
        public void CheckBox(bool isChecked, string name, string text)
        {
            Response.Write("<div class=\"on-off-with-label\" id=\"" + name + "\">");
            Response.Write("	<a href=\"javascript:;\" onclick=\"onOff(this);\" class=\"on-off-button");
            if (isChecked)
                Response.Write(" on");
            Response.Write("\">");
            Response.Write(text);
            Response.Write("</a>");
            Response.Write("	<input type=\"hidden\" name=\"");
            Response.Write(name);
            Response.Write("\" value=\"");
            if (isChecked)
                Response.Write("true");
            else
                Response.Write("false");
            Response.Write("\" />");
            Response.Write("</div>");
        }

        /// <summary>
        /// 复选框组
        /// </summary>
        public void CheckBoxList(List<NtListItem> data, string name)
        {
            if (data == null || data.Count < 1) return;
            Response.Write("<div class=\"check-box-list\" id=\"" + name + "\" data-field-name=\"" + name + "\">");
            Response.Write("	<ul class=\"check-box-list-data\">");
            foreach (var item in data)
            {
                Response.Write("		<li class=\"cbl-item");
                if (item.Selected)
                    Response.Write(" cbl-selected");
                Response.Write("\" data-value=\"");
                Response.Write(item.Value);
                Response.Write("\">");
                Response.Write("			<span>");
                Response.Write(item.Text);
                Response.Write("</span>");
                if (item.Selected)
                {
                    Response.Write("			<input type=\"hidden\" name=\"");
                    Response.Write(name);
                    Response.Write("\" value=\"");
                    Response.Write(item.Value);
                    Response.Write("\" />");
                }
                Response.Write("		</li>");
            }
            Response.Write("	</ul>");
            Response.Write("</div>");
        }

        /// <summary>
        /// 男Or女
        /// </summary>
        public void XY(bool isBoy, string name)
        {
            Response.Write("<div class=\"radio-button-list\" id=\"" + name + "\">");
            Response.Write("	<ul class=\"radio-button-list-data\">");
            Response.Write("		<li class=\"rbl-item" + (isBoy ? " rbl-current" : "") + "\" data-value=\"true\">男</li>");
            Response.Write("		<li class=\"rbl-item" + (isBoy ? "" : " rbl-current") + "\" data-value=\"false\">女</li>");
            Response.Write("	</ul>");
            Response.Write("	<input type=\"hidden\" name=\"");
            Response.Write(name);
            Response.Write("\" value=\"");
            Response.Write(isBoy ? "true" : "false");
            Response.Write("\" />");
            Response.Write("</div>");
        }

        /// <summary>
        /// a or b
        /// </summary>
        public void XY(bool current, string name, string a, string b)
        {
            Response.Write("<div class=\"radio-button-list\" id=\"" + name + "\">");
            Response.Write("	<ul class=\"radio-button-list-data\">");
            Response.Write("		<li class=\"rbl-item" + (current ? " rbl-current" : "") + "\" data-value=\"true\">" + a + "</li>");
            Response.Write("		<li class=\"rbl-item" + (current ? "" : " rbl-current") + "\" data-value=\"false\">" + b + "</li>");
            Response.Write("	</ul>");
            Response.Write("	<input type=\"hidden\" name=\"");
            Response.Write(name);
            Response.Write("\" value=\"");
            Response.Write(current ? "true" : "false");
            Response.Write("\" />");
            Response.Write("</div>");
        }

        /// <summary>
        /// 单选框组
        /// </summary>
        public void RadioButtonList(List<NtListItem> data, string name)
        {
            if (data == null || data.Count < 1) return;
            string currentValue = string.Empty;
            Response.Write("<div class=\"radio-button-list\" id=\"" + name + "\">");
            Response.Write("	<ul class=\"radio-button-list-data\">");
            foreach (var item in data)
            {
                Response.Write("		<li class=\"rbl-item");
                if (item.Selected)
                {
                    Response.Write(" rbl-current");
                    currentValue = item.Value;
                }
                Response.Write("\" data-value=\"");
                Response.Write(item.Value);
                Response.Write("\">");
                Response.Write(item.Text);
                Response.Write("		</li>");
            }
            Response.Write("	</ul>");
            Response.Write("	<input type=\"hidden\" name=\"");
            Response.Write(name);
            Response.Write("\" value=\"");
            Response.Write(currentValue);
            Response.Write("\" />");
            Response.Write("</div>");
        }

        /// <summary>
        /// 日期选择
        /// </summary>
        /// <param name="specified">指定日期</param>
        /// <param name="name">名</param>
        public void DateTimePicker(DateTime specified, string name)
        {
            Response.Write("<input");
            Response.Write(" type=\"text\" title=\"请勿修改日期时间格式,严格遵循yyyy-MM-dd hh:mm:ss日期格式\" class=\"input-datetime\" readonly=\"readonly\" maxlength=\"20\"");
            Response.Write(" value=\"");
            Response.Write(specified.ToString("yyyy-MM-dd hh:mm:ss"));
            Response.Write("\"");
            Response.Write(" name=\"");
            Response.Write(name);
            Response.Write("\" id=\"" + name + "\"/>");
        }

        /// <summary>
        /// 生日
        /// </summary>
        public void BirthDay(DateTime specified, string name)
        {
            Response.Write("<input");
            Response.Write(" type=\"text\" title=\"请勿修改日期时间格式,严格遵循yyyy-MM-dd日期格式\" class=\"input-datetime\" readonly=\"readonly\" maxlength=\"20\"");
            Response.Write(" value=\"");
            Response.Write(specified.ToString("yyyy-MM-dd"));
            Response.Write("\"");
            Response.Write(" name=\"");
            Response.Write(name);
            Response.Write("\" id=\"" + name + "\"/>");
        }

        /// <summary>
        /// 隐藏字段
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="name">键</param>
        public void Hidden(object value, string name)
        {
            Response.Write("<input name=\"");
            Response.Write(name);
            Response.Write("\" value=\"");
            Response.Write(value);
            Response.Write("\" type=\"hidden\"/>");
        }

        void AppendAttrs(object props)
        {
            if (props == null)
                return;
            string name;
            object value;
            foreach (var item in props.GetType().GetProperties())
            {
                name = item.Name;
                value = item.GetValue(props, null);
                if (name[0] == '_')
                    name = name.Remove(0, 1);
                Response.Write(" ");
                Response.Write(name);
                Response.Write("=\"");
                Response.Write(value);
                Response.Write("\"");
            }
        }

    }
}
