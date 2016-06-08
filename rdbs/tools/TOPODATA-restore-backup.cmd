call TOPODATA-config.cmd

%PGBIN%\psql.exe ^
	-f %SCHEMA_FILE% ^
	postgres
