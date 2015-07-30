using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Model;
using Nt.Model.Common;
using Nt.Model.Setting;
using Nt.Model.View;
using Nt.DAL;
using Nt.BLL;

namespace Nt.Framework.Admin
{
    public abstract class SettingBase<S> : PageBase
        where S : BaseSetting, new()
    {

        protected abstract void AfterPost();
        protected abstract void AfterGet();

        protected string langCode;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            langCode = NtContext.Current.CurrentLanguage.LanguageCode;
            if (IsHttpPost)
            {
                SettingService<S> ss = new SettingService<S>(langCode);
                var m = new S();
                m.InitDataFromPage();
                ss.SaveSetting(m);
                AfterPost();
                Goto(Request.RawUrl, "设置保存成功!");
            }

            Model = SettingService.GetSettingModel<S>(langCode);
            AfterGet();
        }

        public S Model { get; set; }
    }
}
