using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Netin_Shared_editor : UserControl
{
    string _textareaName = "Body";
    /// <summary>
    /// 应用编辑器的元素的Name
    /// </summary>
    public string TextareaName
    {
        get { return _textareaName; }
        set { _textareaName = value; }
    }

    public bool SimpleEditor { get; set; }

    public string Items
    {
        get
        {
            if (!SimpleEditor)
                return "\"source\", \"|\", \"undo\", \"redo\", \"|\", \"preview\", \"print\", \"template\", \"code\", \"cut\", \"copy\", \"paste\", \"plainpaste\", \"wordpaste\", \"|\",\"justifyleft\", \"justifycenter\", \"justifyright\", \"justifyfull\", \"insertorderedlist\", \"insertunorderedlist\", \"indent\", \"outdent\",\"subscript\", \"superscript\", \"clearhtml\", \"quickformat\", \"selectall\", \"|\", \"fullscreen\", \"/\", \"formatblock\", \"fontname\",\"fontsize\", \"|\", \"forecolor\", \"hilitecolor\", \"bold\", \"italic\", \"underline\", \"strikethrough\", \"lineheight\", \"removeformat\",\"|\", \"image\", \"multiimage\", \"flash\", \"media\", \"insertfile\", \"table\", \"hr\", \"emoticons\", \"baidumap\", \"pagebreak\", \"anchor\",\"link\", \"unlink\", \"|\", \"about\"";
            else
                return "\"source\", \"|\", \"undo\", \"redo\", \"|\", \"cut\", \"copy\", \"paste\", \"plainpaste\",\"|\",\"justifyleft\", \"justifycenter\", \"justifyright\", \"justifyfull\", \"insertorderedlist\", \"insertunorderedlist\", \"indent\", \"outdent\",\"clearhtml\", \"quickformat\", \"selectall\", \"|\",\"/\", \"formatblock\", \"fontname\",\"fontsize\", \"|\", \"forecolor\", \"hilitecolor\", \"bold\", \"italic\", \"underline\", \"strikethrough\", \"lineheight\", \"removeformat\",\"|\",\"about\"";
        }
    }

}