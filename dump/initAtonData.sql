--
-- PostgreSQL database dump
--

-- Dumped from database version 16.1
-- Dumped by pg_dump version 16.1

-- Started on 2024-04-23 17:59:02

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 215 (class 1259 OID 17899)
-- Name: user; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."user" (
    guid uuid NOT NULL,
    login character varying NOT NULL,
    password character varying NOT NULL,
    name character varying NOT NULL,
    gender integer NOT NULL,
    birthday timestamp with time zone,
    admin boolean NOT NULL,
    createdon timestamp with time zone NOT NULL,
    createdby character varying NOT NULL,
    modifiedon timestamp with time zone,
    modifiedby character varying,
    revokedon timestamp with time zone,
    revokedby character varying,
    CONSTRAINT login_check CHECK (((login)::text ~ '^[a-zA-Z0-9]+$'::text))
);


--
-- TOC entry 4842 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN "user".guid; Type: COMMENT; Schema: public; Owner: -
--

COMMENT ON COLUMN public."user".guid IS 'Уникальный идентификатор пользователя';


--
-- TOC entry 4843 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN "user".login; Type: COMMENT; Schema: public; Owner: -
--

COMMENT ON COLUMN public."user".login IS 'Уникальный Логин (запрещены все символы кроме латинских букв и цифр)';


--
-- TOC entry 4844 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN "user".password; Type: COMMENT; Schema: public; Owner: -
--

COMMENT ON COLUMN public."user".password IS 'Пароль(запрещены все символы кроме латинских букв и цифр)';


--
-- TOC entry 4845 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN "user".name; Type: COMMENT; Schema: public; Owner: -
--

COMMENT ON COLUMN public."user".name IS 'Имя (запрещены все символы кроме латинских и русских букв)';


--
-- TOC entry 4846 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN "user".gender; Type: COMMENT; Schema: public; Owner: -
--

COMMENT ON COLUMN public."user".gender IS 'Пол 0 - женщина, 1 - мужчина, 2 - неизвестно';


--
-- TOC entry 4847 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN "user".birthday; Type: COMMENT; Schema: public; Owner: -
--

COMMENT ON COLUMN public."user".birthday IS 'Поле даты рождения может быть Null';


--
-- TOC entry 4848 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN "user".admin; Type: COMMENT; Schema: public; Owner: -
--

COMMENT ON COLUMN public."user".admin IS 'Указание - является ли пользователь админом';


--
-- TOC entry 4849 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN "user".createdon; Type: COMMENT; Schema: public; Owner: -
--

COMMENT ON COLUMN public."user".createdon IS 'Дата создания пользователя';


--
-- TOC entry 4850 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN "user".createdby; Type: COMMENT; Schema: public; Owner: -
--

COMMENT ON COLUMN public."user".createdby IS 'Логин Пользователя, от имени которого этот пользователь создан';


--
-- TOC entry 4851 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN "user".modifiedon; Type: COMMENT; Schema: public; Owner: -
--

COMMENT ON COLUMN public."user".modifiedon IS 'Дата изменения пользователя';


--
-- TOC entry 4852 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN "user".modifiedby; Type: COMMENT; Schema: public; Owner: -
--

COMMENT ON COLUMN public."user".modifiedby IS 'Логин Пользователя, от имени которого этот пользователь изменён';


--
-- TOC entry 4853 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN "user".revokedon; Type: COMMENT; Schema: public; Owner: -
--

COMMENT ON COLUMN public."user".revokedon IS 'Дата удаления пользователя';


--
-- TOC entry 4854 (class 0 OID 0)
-- Dependencies: 215
-- Name: COLUMN "user".revokedby; Type: COMMENT; Schema: public; Owner: -
--

COMMENT ON COLUMN public."user".revokedby IS 'Логин Пользователя, от имени которого этот пользователь удалён';


--
-- TOC entry 4835 (class 0 OID 17899)
-- Dependencies: 215
-- Data for Name: user; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."user" VALUES ('cdc6b9fe-9b7b-4dfe-a707-43e7036dcf21', 'aleksandr', '1243', 'aleksandr', 1, '2001-04-23 13:22:21.774+04', true, '2024-04-23 11:50:26.327641+03', 'alex', '2024-04-23 12:25:17.757461+03', 'aleksandr', NULL, NULL);


--
-- TOC entry 4689 (class 2606 OID 17906)
-- Name: user user_pk; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_pk PRIMARY KEY (guid);


--
-- TOC entry 4691 (class 2606 OID 17932)
-- Name: user user_un; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_un UNIQUE (login);


-- Completed on 2024-04-23 17:59:02

--
-- PostgreSQL database dump complete
--