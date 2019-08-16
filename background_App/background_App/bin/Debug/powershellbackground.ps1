param(
    [Parameter(Mandatory=$True,Position=1)]
    [string]$filename
)

$myPicture = "$filename.jpg"
 
Write-Host "$myPicture"

# Setting wallpaper to the regisrty. 
Set-ItemProperty -path 'HKCU:\Control Panel\Desktop\' -name wallpaper -value $myPicture 
 
# updating the user settings 
rundll32.exe user32.dll, UpdatePerUserSystemParameters  
rundll32.exe user32.dll, UpdatePerUserSystemParameters  
rundll32.exe user32.dll, UpdatePerUserSystemParameters 
rundll32.exe user32.dll, UpdatePerUserSystemParameters  
rundll32.exe user32.dll, UpdatePerUserSystemParameters  
rundll32.exe user32.dll, UpdatePerUserSystemParameters 
rundll32.exe user32.dll, UpdatePerUserSystemParameters  
rundll32.exe user32.dll, UpdatePerUserSystemParameters  
rundll32.exe user32.dll, UpdatePerUserSystemParameters 


Write-Host "The background image has been updated!!"

#To run the script in cmd use this command
#powershell -ExecutionPolicy ByPass -File powershellBackground.ps1 -filename 20