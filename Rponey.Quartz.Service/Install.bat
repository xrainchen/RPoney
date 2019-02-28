cd /d %~dp0 
sc create "Rponey.Quartz.Services" binpath= "%cd%\Rponey.Quartz.Service.exe"  start= auto
sc description "Rponey.Quartz.Services" "Rponey 定时 服务"

net start "Rponey.Quartz.Services"
pause