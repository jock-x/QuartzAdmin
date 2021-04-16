color 3

dotnet new -i .template.config\BiliFor.Webapi.Template.2.5.2.nupkg

set /p OP=Please set your project name(for example:Baidu.Api):

md .1YourProject

cd .1YourProject

dotnet new blogcoretpl -n %OP%

cd ../


echo "Create Successfully!!!! ^ please see the folder .1YourProject"

dotnet new -u BiliFor.Webapi.Template


echo "Delete Template Successfully"

pause