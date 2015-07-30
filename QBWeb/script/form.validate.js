/// <reference path="jquery-1.7.2.min.js" />
/// <reference path="naite.utility.js" />
/*
用于前端验证表单的函数---留言
*/
function validateGuestBookForm(form) {
    if (!form)
        return false;
    var validList = [
        form.Title, '标题',
        form.Name, '姓名',
        form.Tel, '固话',
        form.NativePlace, '祖籍',
        form.Nation, '民族',
        form.PersonID, '身份证',
        form.EduDegree, '学历',
        form.ZipCode, '邮编',
        form.PoliticalRole, '政治身份',
        form.Address, '住址',
        form.GraduatedFrom, '毕业学校',
        form.Grade, '成绩',
        form.BirthDate, '生日',
        form.Mobile, '手机',
        form.Email, '电子邮件',
        form.Company, '公司',
        form.Body, '留言'
    ];
    for (var i = 0; i < validList.length;) {
        var inp = validList[i++];
        var msg = validList[i++];
        if (inp != undefined) {
            if (inp.value == '') {
                alert(msg + '不能为空', function () {
                    inp.focus();
                    redBorder(inp);
                });
                return false;
            }
        }
    }    
    return true;
}

/*注册表单验证*/
function validateRegisterForm(form) {
    if (!form)
        return false;

    if (form.RealName && form.RealName.value == '') {
        alert('姓名不可以为空!');
        form.RealName.focus();
        redBorder(form.RealName);
        return false;
    }

    if (form.LoginName && !/^[a-zA-Z][a-zA-Z0-9]*$/.test(form.LoginName.value)) {
        alert('登陆名错误，以字母开头，4-20位字母或数字!');
        form.LoginName.focus();
        redBorder(form.LoginName);
        return false;
    }

    if (form.Password.value.length < 6) {
        alert('密码不应少于6个字符!');
        form.Password.focus();
        redBorder(form.Password);
        return false;
    }

    if (form.Password.value != form['Password.Again'].value) {
        alert('两次密码输入不一致!');
        form['Password.Again'].focus();
        redBorder(form['Password.Again']);
        return false;
    }

    if (form.ZipCode && !/^\d{6}$/.test(form.ZipCode.value)) {
        alert('邮编格式不正确!');
        form.ZipCode.focus();
        redBorder(form.ZipCode);
        return false;
    }

    if (form.Email && !checkEmail(form.Email.value)) {
        alert('邮箱格式不正确');
        form.Email.focus();
        redBorder(form.Email);
        return false;
    }

    if (form.MobilePhone && !(
        checkPhone(form.MobilePhone.value)
        || checkMobile(form.MobilePhone.value))) {
        alert('手机格式不正确!');
        form.MobilePhone.focus();
        redBorder(form.MobilePhone);
        return false;
    }

    if (form.Phone && !(
        checkPhone(form.Phone.value)
        || checkMobile(form.Phone.value))) {
        alert('电话格式不正确!');
        form.Phone.focus();
        redBorder(form.Phone);
        return false;
    }

    if (form.CheckCode && form.CheckCode.value == '') {
        alert('验证码不能为空');
        form.CheckCode.focus();
        redBorder(form.CheckCode);
        return false;
    }
    form.submit();
    return true;
}

function redBorder(n) {
    n.style.border = '1px solid red';
}