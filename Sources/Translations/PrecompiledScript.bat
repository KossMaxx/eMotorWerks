echo "convert %1i18n\common\messages.txt"
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\resgen.exe" "%1i18n\common\messages.txt" "%1i18n\common\messages.resx" /str:cs,Translations.i18n.common,messages,"%1i18n\common\messages.Designer.cs" /publicclass

echo "convert %1i18n\modules\layouts\home\messages.txt"
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\resgen.exe" "%1i18n\modules\layouts\home\messages.txt" "%1i18n\modules\layouts\home\messages.resx" /str:cs,Translations.i18n.modules.layouts.home,messages,"%1i18n\modules\layouts\home\messages.Designer.cs" /publicclass

echo "convert %1i18n\modules\layouts\login\messages.txt"
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\resgen.exe" "%1i18n\modules\layouts\login\messages.txt" "%1i18n\modules\layouts\login\messages.resx" /str:cs,Translations.i18n.modules.layouts.login,messages,"%1i18n\modules\layouts\login\messages.Designer.cs" /publicclass

echo "convert %1i18n\modules\layouts\sidebar\messages.txt"
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\resgen.exe" "%1i18n\modules\layouts\sidebar\messages.txt" "%1i18n\modules\layouts\sidebar\messages.resx" /str:cs,Translations.i18n.modules.layouts.sidebar,messages,"%1i18n\modules\layouts\sidebar\messages.Designer.cs" /publicclass

echo "convert %1i18n\modules\layouts\totalpages\messages.txt"
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\resgen.exe" "%1i18n\modules\layouts\totalpages\messages.txt" "%1i18n\modules\layouts\totalpages\messages.resx" /str:cs,Translations.i18n.modules.layouts.totalpages,messages,"%1i18n\modules\layouts\totalpages\messages.Designer.cs" /publicclass

echo "convert %1i18n\modules\core\accounts\controller\messages.txt"
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\resgen.exe" "%1i18n\modules\core\accounts\controller\messages.txt" "%1i18n\modules\core\accounts\controller\messages.resx" /str:cs,Translations.i18n.modules.core.accounts.controller,messages,"%1i18n\modules\core\accounts\controller\messages.Designer.cs" /publicclass

echo "convert %1i18n\modules\site\home\controller\messages.txt"
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\resgen.exe" "%1i18n\modules\site\home\controller\messages.txt" "%1i18n\modules\site\home\controller\messages.resx" /str:cs,Translations.i18n.modules.site.home.controller,messages,"%1i18n\modules\site\home\controller\messages.Designer.cs" /publicclass

echo "convert %1i18n\modules\core\accounts\login\messages.txt"
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\resgen.exe" "%1i18n\modules\core\accounts\login\messages.txt" "%1i18n\modules\core\accounts\login\messages.resx" /str:cs,Translations.i18n.modules.core.accounts.login,messages,"%1i18n\modules\core\accounts\login\messages.Designer.cs" /publicclass

echo "convert %1i18n\modules\site\home\index\messages.txt"
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\resgen.exe" "%1i18n\modules\site\home\index\messages.txt" "%1i18n\modules\site\home\index\messages.resx" /str:cs,Translations.i18n.modules.site.home.index,messages,"%1i18n\modules\site\home\index\messages.Designer.cs" /publicclass

echo "convert %1i18n\modules\site\totalroles\index\messages.txt"
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\resgen.exe" "%1i18n\modules\site\totalroles\index\messages.txt" "%1i18n\modules\site\totalroles\index\messages.resx" /str:cs,Translations.i18n.modules.site.totalroles.index,messages,"%1i18n\modules\site\totalroles\index\messages.Designer.cs" /publicclass

echo "convert %1i18n\modules\site\totalusers\index\messages.txt"
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\resgen.exe" "%1i18n\modules\site\totalusers\index\messages.txt" "%1i18n\modules\site\totalusers\index\messages.resx" /str:cs,Translations.i18n.modules.site.totalusers.index,messages,"%1i18n\modules\site\totalusers\index\messages.Designer.cs" /publicclass

echo "convert %1i18n\modules\site\detailsrole\index\messages.txt"
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\resgen.exe" "%1i18n\modules\site\detailsrole\index\messages.txt" "%1i18n\modules\site\detailsrole\index\messages.resx" /str:cs,Translations.i18n.modules.site.detailsrole.index,messages,"%1i18n\modules\site\detailsrole\index\messages.Designer.cs" /publicclass

echo "convert %1i18n\modules\site\detailsuser\index\messages.txt"
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\resgen.exe" "%1i18n\modules\site\detailsuser\index\messages.txt" "%1i18n\modules\site\detailsuser\index\messages.resx" /str:cs,Translations.i18n.modules.site.detailsuser.index,messages,"%1i18n\modules\site\detailsuser\index\messages.Designer.cs" /publicclass