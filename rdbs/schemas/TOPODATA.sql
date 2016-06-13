--
-- PostgreSQL database dump
--

-- Dumped from database version 9.5.3
-- Dumped by pg_dump version 9.5.3

SET statement_timeout = 0;
SET lock_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: TOPODATA; Type: DATABASE; Schema: -; Owner: topodata
--

DROP DATABASE IF EXISTS "TOPODATA";

CREATE DATABASE "TOPODATA" WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'German_Germany.1252' LC_CTYPE = 'German_Germany.1252';


ALTER DATABASE "TOPODATA" OWNER TO topodata;

\connect "TOPODATA"

SET statement_timeout = 0;
SET lock_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET search_path = public, pg_catalog;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- Name: exportstatistic; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE exportstatistic (
    cid integer,
    zaehltag timestamp without time zone,
    dateiname text,
    exportzeitpunkt timestamp without time zone
);


ALTER TABLE exportstatistic OWNER TO topodata;

--
-- Name: BinaryData_Import_Statistic; Type: TABLE; Schema: public; Owner: topodata; Tablespace: 
--

CREATE TABLE "BinaryData_Import_Statistic" (
    "BinaryData_ControllerId" integer DEFAULT (-1) NOT NULL,
    "BinaryData_EndDate" timestamp with time zone DEFAULT '1970-01-01 00:00:00+01'::timestamp with time zone NOT NULL,
    "BinaryData_BeginDate" timestamp without time zone DEFAULT '1970-01-01 00:00:00'::timestamp without time zone NOT NULL,
    "BinaryData_ImportDate" timestamp without time zone DEFAULT '1970-01-01 00:00:00'::timestamp without time zone NOT NULL,
    "BinaryData_DataCount_DebugTurnus" integer DEFAULT (-1) NOT NULL
);

ALTER TABLE public."BinaryData_Import_Statistic" OWNER TO topodata;

INSERT INTO public."BinaryData_Import_Statistic" (
	"BinaryData_ControllerId",
	"BinaryData_BeginDate",
	"BinaryData_EndDate",
	"BinaryData_ImportDate",
	"BinaryData_DataCount_DebugTurnus"
	)
	VALUES
	(1000, '2016-01-01 00:00:00'::timestamp, '2016-01-01 03:00:00'::timestamp, '2016-01-02 05:00:00'::timestamp, 10),
	(1000, '2016-01-01 03:00:00', '2016-01-01 23:59:00', '2016-01-02 05:00:00', 100),
	(2000, '2016-01-01 00:00:00', '2016-01-01 03:00:00', '2016-01-02 05:00:00', 11),
	(2000, '2016-01-01 03:00:00', '2016-01-01 23:59:00', '2016-01-02 05:00:00', 110)
	;

--
-- Name: BinaryData_Export_Statistic; Type: TABLE; Schema: public; Owner: topodata; Tablespace: 
--

CREATE TABLE public."BinaryData_Export_Statistic" (
    "BinaryData_ControllerId" integer NOT NULL,
    "BinaryData_EndDate" timestamp without time zone DEFAULT '1970-01-01 00:00:00'::timestamp without time zone NOT NULL,
    "BinaryData_BeginDate" timestamp without time zone DEFAULT '1970-01-01 00:00:00'::timestamp without time zone NOT NULL,
    "BinaryData_ExportDate" timestamp without time zone DEFAULT '1970-01-01 00:00:00'::timestamp without time zone NOT NULL,
    "BinaryData_FileIdx" integer DEFAULT 0 NOT NULL
);


ALTER TABLE public."BinaryData_Export_Statistic" OWNER TO topodata;

INSERT INTO public."BinaryData_Export_Statistic" (
	"BinaryData_ControllerId",
	"BinaryData_BeginDate",
	"BinaryData_EndDate",
	"BinaryData_ExportDate",
	"BinaryData_FileIdx"
	)
	VALUES
	(1000, '2016-01-01 00:00:00'::timestamp, '2016-01-01 17:00:00'::timestamp, '2016-01-02 05:00:00'::timestamp, 1),
	(1000, '2016-01-01 00:00:00', '2016-01-01 23:59:00', '2016-01-03 05:10:00', 2),
	(2000, '2016-01-01 00:00:00', '2016-01-01 23:58:00', '2016-01-03 05:05:00', 1)
	;
	
--
-- Name: BinaryData_DebugTurnus; Type: TABLE; Schema: public; Owner: topodata; Tablespace: 
--

CREATE TABLE "BinaryData_DebugTurnus" (
    "BinaryData_CorrectionDate" timestamp without time zone,
    "BinaryData_ControllerId" smallint,
    "BinaryData_DataType" smallint,
    "BinaryData_IgnoreType" smallint,
    "BinaryData_Data" bytea,
    "BinaryData_VehicleType" smallint,
    "BinaryData_Block" smallint,
    "BinaryData_Line" smallint,
    "BinaryData_FileName_Ref" integer DEFAULT (-1)
);

ALTER TABLE public."BinaryData_DebugTurnus" OWNER TO topodata;


--
-- Name: BinaryData_Controller; Type: TABLE; Schema: public; Owner: topodata; Tablespace: 
--

CREATE TABLE "BinaryData_Controller" (
    "BinaryData_ControllerId" smallint NOT NULL,
    "BinaryData_Ignore_MeasurementHeader" boolean DEFAULT false,
    "BinaryData_StartDate" timestamp without time zone,
    "BinaryData_ExportFolder" text,
    "BinaryData_ExportLanguage" text,
    "BinaryData_EndDate" timestamp without time zone,
    "BinaryData_ControllerIdExport" smallint,
    "BinaryData_ClassCode" smallint,
    "BinaryData_ExportFormat" smallint DEFAULT 0,
    "ID" integer NOT NULL,
    "BinaryData_EditDate" timestamp without time zone DEFAULT now()
);


ALTER TABLE public."BinaryData_Controller" OWNER TO topodata;

--
-- Name: BinaryData_Export_Files; Type: TABLE; Schema: public; Owner: topodata; Tablespace: topodata_san
--

CREATE TABLE "BinaryData_Export_Files" (
    "BinaryData_Export_FileName" text,
    "BinaryData_Date" timestamp with time zone,
    "BinaryData_ControllerId" smallint,
    "BinaryData_Export_FileData" text,
    "ID" integer NOT NULL
);


ALTER TABLE public."BinaryData_Export_Files" OWNER TO topodata;

CREATE SCHEMA pgmssql AUTHORIZATION topodata;

--
-- Name: BinaryData_Controller_Types; Type: TABLE; Schema: pgmssql; Owner: topodata; Tablespace: 
--

CREATE TABLE pgmssql."BinaryData_Controller_Types" (
    "ControllerType" text,
    "ID" integer NOT NULL,
    aggregation_type_id smallint
);

ALTER TABLE pgmssql."BinaryData_Controller_Types" OWNER TO topodata;

COPY pgmssql."BinaryData_Controller_Types" ("ControllerType", "ID", aggregation_type_id) FROM stdin;
Type 1	0	1
Type 2	1	2
Type 3	2	3
Type 4	3	4
\.

--
-- Name: BinaryData_Controller; Type: TABLE; Schema: pgmssql; Owner: topodata; Tablespace: 
--

CREATE TABLE pgmssql."BinaryData_Controller" (
    "BinaryData_ControllerId" smallint NOT NULL,
    "BinaryData_ControllerType" smallint
);

ALTER TABLE pgmssql."BinaryData_Controller" OWNER TO topodata;

COPY pgmssql."BinaryData_Controller" ("BinaryData_ControllerId", "BinaryData_ControllerType") FROM stdin;
293	4
370	3
401	2
405	1
428	4
\.


--
-- Name: public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE ALL ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM postgres;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO PUBLIC;


--
-- PostgreSQL database dump complete
--

