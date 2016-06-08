call TOPODATA-config.cmd

%PGBIN%\pg_dump.exe ^
	--create ^
	--file=%SCHEMA_FILE% ^
	TOPODATA
