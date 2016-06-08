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
-- Name: importstatistic; Type: TABLE; Schema: public; Owner: topodata
--

CREATE TABLE importstatistic (
    cid integer,
    begin_date timestamp without time zone,
    end_date timestamp without time zone,
    n_vehicles integer,
    dateiname text
);


ALTER TABLE importstatistic OWNER TO topodata;

--
-- Data for Name: exportstatistic; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY exportstatistic (cid, zaehltag, dateiname, exportzeitpunkt) FROM stdin;
1000	2016-01-01 00:00:00	052_1000_1.txt	2016-01-02 07:00:00
2000	2016-01-02 00:00:00	052_2000_1.txt	2016-01-03 07:00:00
\.


--
-- Data for Name: importstatistic; Type: TABLE DATA; Schema: public; Owner: topodata
--

COPY importstatistic (cid, begin_date, end_date, n_vehicles, dateiname) FROM stdin;
1000	2016-01-01 00:00:00	2016-01-01 03:00:00	100	Stat_1000_1.bin
1000	2016-01-01 03:00:01	2016-01-01 23:59:00	999	Stat_1000_2.bin
2000	2016-01-02 00:00:00	2016-01-02 03:00:00	100	Stat_2000_1.bin
2000	2016-01-02 03:00:01	2016-01-02 23:59:00	999	Stat_2000_2.bin
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

