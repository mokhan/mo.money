$local_settings = @{
  	debug = "false";
  	log_level = "DEBUG";
	assembly_title = "momoney"
	assembly_description = "a sample application to tinker with"
	assembly_company = "http://mokhan.ca"
#	assembly_version = "${datetime::get-year(datetime::now())}.${datetime::get-month(datetime::now())}.${datetime::get-day(datetime::now())}.${datetime::get-hour(datetime::now())}${datetime::get-minute(datetime::now())}" />
	editor_exe  = "notepad.exe"
  
	dot_net_sdk_dir = "C:\Program Files\Microsoft SDKs\Windows\v6.0A\bin"
	framework_dir="c:\windows\microsoft.net\framework\v3.5"
	deployment_url="http://mokhan.ca/mymoney/"
  
	certificate_filename="$build_config_dir\mokhan.pfx"
	certificate_password="" 
  
	browser_exe="c:\program files\mozilla firefox\firefox.exe"
  }