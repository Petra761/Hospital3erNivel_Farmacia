--
-- PostgreSQL database dump
--

\restrict 9WR2OsmPmzsODWE8CfvXaXIwwkOTTZpym5p8bWNPPA6ymOF51dbM7WYJrgC2Sr8

-- Dumped from database version 16.13 (Ubuntu 16.13-0ubuntu0.24.04.1)
-- Dumped by pg_dump version 16.13 (Ubuntu 16.13-0ubuntu0.24.04.1)

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

--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20260426231719_v0	8.0.23
20260501020339_v1	8.0.23
20260501042057_v1.1	8.0.23
20260501161132_v2	8.0.23
20260501232753_v2.1	8.0.23
20260502022946_v2.2	8.0.23
20260504235036_v3	8.0.23
20260505002651_v3.1	8.0.23
20260505002740_v3.2	8.0.23
20260505215503_v3.3	8.0.23
\.


--
-- Data for Name: formas_farmaceuticas; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.formas_farmaceuticas (id, nombre, estado) FROM stdin;
1	Tableta	Activo
2	Jarabe	Activo
3	Cápsula	Activo
4	Suspensión Inyectable	Activo
5	Crema	Inactivo
\.


--
-- Data for Name: tipos_medicamentos; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.tipos_medicamentos (id, codigo, nombre_generico, nombre_comercial, es_controlado, requiere_refrigeracion, stock_minimo_alerta, estado) FROM stdin;
1	TMED-PAR-A1B2	Paracetamol	Panadol	f	f	100	Activo
2	TMED-AMO-C3D4	Amoxicilina	Amoxil	f	f	50	Activo
3	TMED-INS-E5F6	Insulina	Lantus	f	t	20	Activo
4	TMED-CLO-G7H8	Clonazepam	Rivotril	t	f	15	Activo
5	TMED-IBU-FV5C	Ibuprofeno	Advil	f	f	40	Activo
6	TMED-MET-J7PV	Metformina	Glucophage	f	f	60	Activo
7	TMED-LOR-ZU4B	Loratadina	Claritin	f	f	40	Activo
8	TMED-OME-O2MU	Omeprazol	Losec	f	f	80	Activo
9	TMED-MOR-UYG8	Morfina	MST Continus	t	f	10	Activo
10	TMED-VAC-U1B1	Vacuna Antigripal	Vaxigrip	f	t	30	Activo
11	TMED-DIA-XE3Y	Diazepam	Valium	t	f	20	Activo
12	TMED-SAL-2X7Q	Salbutamol	Ventolin	f	f	50	Activo
13	TMED-ATO-T7P5	Atorvastatina	Lipitor	f	f	40	Activo
14	TMED-ERI-U6JA	Eritropoyetina	Epogen	f	t	15	Activo
15	TMED-ALP-KXMH	Alprazolam	Xanax	t	f	25	Activo
\.


--
-- Data for Name: tipos_unidad_medida; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.tipos_unidad_medida (id, nombre, abreviatura, estado) FROM stdin;
1	Miligramo	mg	Activo
2	Mililitro	ml	Activo
3	Microgramo	mcg	Activo
4	Unidad	u	Activo
5	Gramos	g	Inactivo
\.


--
-- Data for Name: medicamentos; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.medicamentos (id, codigo, medicamento_id, unidad_medida_id, forma_id, valor_concentracion, estado) FROM stdin;
1	MED-PAN-TAB-MGT3	1	1	1	500	Activo
2	MED-AMO-JAR-EIF1	2	1	2	250	Activo
3	MED-LAN-SUS-ICR6	3	4	4	100	Activo
4	MED-PAN-JAR-Z70J	1	1	2	120	Activo
5	MED-AMO-CÁP-A91N	2	1	3	500	Activo
6	MED-RIV-TAB-B1QT	4	1	1	2	Activo
7	MED-ADV-TAB-3VVG	5	1	1	400	Activo
8	MED-GLU-TAB-ULQS	6	1	1	850	Activo
9	MED-CLA-TAB-4DEV	7	1	1	10	Activo
10	MED-LOS-CÁP-VEJ4	8	1	3	20	Activo
11	MED-MST-SUS-NJHD	9	1	4	10	Activo
12	MED-VAX-SUS-SKJ0	10	2	4	0.5	Activo
13	MED-VAL-TAB-N96Z	11	1	1	10	Activo
14	MED-VEN-JAR-Y7P3	12	1	2	2	Activo
15	MED-LIP-TAB-TMD7	13	1	1	20	Activo
16	MED-EPO-SUS-CY1F	14	4	4	4000	Activo
17	MED-XAN-TAB-NYDD	15	1	1	0.5	Activo
18	MED-ADV-JAR-G4KW	5	1	2	100	Activo
\.


--
-- Data for Name: recepciones; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.recepciones (id, codigo, fecha_recepcion, recibido_por_codigo, estado) FROM stdin;
1	RCP-260501-O3IR	2026-05-01	EMP-ADMIN-01	Completo
2	RCP-260501-WH0H	2026-05-01	EMP-URG-03	Completo
3	RCP-260501-HTQJ	2026-05-01	EMP-WARE-02	Parcial
4	RCP-260505-MZ47	2026-05-05	EMP-ADMIN-001	COMPLETADO
\.


--
-- Data for Name: detalle_recepcion; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.detalle_recepcion (id, recepcion_id, medicamento_id, cantidad_recibida, estado, fecha_vencimiento) FROM stdin;
1	1	1	1000	OK	2028-10-15
2	1	2	500	OK	2027-05-20
3	1	3	200	OK	2026-12-01
4	2	3	150	OK	2027-08-10
5	3	4	50	OK	2027-01-30
6	3	5	100	Dañado	2027-06-15
7	4	1	1000	BUEN ESTADO	2028-12-01
8	4	2	500	BUEN ESTADO	2027-06-15
9	4	3	200	BUEN ESTADO	2027-01-20
10	4	4	400	BUEN ESTADO	2027-11-10
11	4	5	800	BUEN ESTADO	2028-03-25
12	4	6	300	BUEN ESTADO	2028-05-14
13	4	7	1000	BUEN ESTADO	2028-10-10
14	4	8	600	BUEN ESTADO	2028-02-28
15	4	9	500	BUEN ESTADO	2027-09-30
16	4	10	700	BUEN ESTADO	2028-01-15
17	4	11	150	BUEN ESTADO	2027-04-12
18	4	12	200	BUEN ESTADO	2026-12-31
19	4	13	400	BUEN ESTADO	2028-06-20
20	4	14	350	BUEN ESTADO	2027-08-18
21	4	15	500	BUEN ESTADO	2028-04-05
22	4	16	100	BUEN ESTADO	2027-02-14
23	4	17	300	BUEN ESTADO	2028-07-22
24	4	18	450	BUEN ESTADO	2027-11-30
\.


--
-- Data for Name: recetas; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.recetas (id, codigo, paciente_codigo, medico_codigo, fecha_solicitud, estado) FROM stdin;
1	RCT-260505-607J	PAC-771122	MED-DR-LOPEZ	2026-05-05	Entregado
2	RCT-260505-LYO5	PAC-445566	MED-DR-VILLAR	2026-05-05	Entregado
3	RCT-260505-0F7S	PAC-112233	MED-DR-CHAMBI	2026-05-05	Entregado
\.


--
-- Data for Name: detalle_receta; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.detalle_receta (id, receta_id, medicamento_id, cantidad_solicitada, estado) FROM stdin;
1	1	1	10	Entregado Total
2	2	2	2	Entregado Total
3	2	8	30	Entregado Total
4	2	7	15	Entregado Total
5	3	17	10	Entregado Total
6	3	10	60	Entregado Total
\.


--
-- Data for Name: dispensacion; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.dispensacion (id, codigo, receta_id, farmaceutico_codigo, quien_recoge, fecha, estado) FROM stdin;
1	DSP-260505-5CSX	1	FARM-001	\N	2026-05-05	Completado
2	DSP-260505-TWOB	2	FARM-001	\N	2026-05-05	Completado
3	DSP-260505-QNT1	3	FARM-SISTEMA-01	ENF-001	2026-05-05	Completado
\.


--
-- Data for Name: lotes; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.lotes (id, codigo, medicamento_id, detalle_recepcion_id, cantidad_inicial, fecha_ingreso, estado) FROM stdin;
11	LOT-281015-IS7S	1	1	1000	2026-05-01	Disponible
12	LOT-270520-L3QF	2	2	500	2026-05-01	Disponible
13	LOT-261201-6CKU	3	3	200	2026-05-01	Disponible
14	LOT-270810-IMSO	3	4	150	2026-05-01	Disponible
15	LOT-270130-KSZ8	4	5	50	2026-05-01	Disponible
16	LOT-281201-YCG0	1	7	1000	2026-05-05	Disponible
17	LOT-270615-AJAK	2	8	500	2026-05-05	Disponible
18	LOT-270120-8TEU	3	9	200	2026-05-05	Disponible
19	LOT-271110-H0LV	4	10	400	2026-05-05	Disponible
20	LOT-280325-9RHA	5	11	800	2026-05-05	Disponible
21	LOT-280514-YT5C	6	12	300	2026-05-05	Disponible
22	LOT-281010-6JOM	7	13	1000	2026-05-05	Disponible
23	LOT-280228-CZW1	8	14	600	2026-05-05	Disponible
24	LOT-270930-Z85X	9	15	500	2026-05-05	Disponible
25	LOT-280115-I84W	10	16	700	2026-05-05	Disponible
26	LOT-270412-UX3Q	11	17	150	2026-05-05	Disponible
27	LOT-261231-D2U2	12	18	200	2026-05-05	Disponible
28	LOT-280620-Z80K	13	19	400	2026-05-05	Disponible
29	LOT-270818-G4MG	14	20	350	2026-05-05	Disponible
30	LOT-280405-SC5E	15	21	500	2026-05-05	Disponible
31	LOT-270214-Z5SF	16	22	100	2026-05-05	Disponible
32	LOT-280722-I2TG	17	23	300	2026-05-05	Disponible
33	LOT-271130-C4K2	18	24	450	2026-05-05	Disponible
\.


--
-- Data for Name: ubicaciones_almacen; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.ubicaciones_almacen (id, codigo, nombre, estado) FROM stdin;
1	UBIC-EST-OMYQ	Estante A	Activo
2	UBIC-EST-GP57	Estante B	Activo
3	UBIC-REF-9U8F	Refrigerador Principal	Activo
4	UBIC-CAJ-G5UK	Caja de Seguridad	Activo
5	UBIC-MOS-T44V	Mostrador	Activo
6	UBIC-ALM-2G80	Almacén de Cuarentena	Activo
7	UBIC-REC-001	ÁREA DE RECEPCIÓN	Activo
\.


--
-- Data for Name: stock_actual; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.stock_actual (id, lote_id, ubicacion_id, cantidad, estado) FROM stdin;
10	14	7	150	Activo
12	16	7	1000	Activo
13	17	7	500	Activo
15	19	7	400	Activo
16	20	7	800	Activo
17	21	7	300	Activo
20	24	7	500	Activo
22	26	7	150	Activo
24	28	7	400	Activo
25	29	7	350	Activo
26	30	7	500	Activo
29	33	7	450	Activo
7	11	7	990	Activo
8	12	7	498	Activo
18	22	7	985	Activo
19	23	7	570	Activo
21	25	7	640	Activo
28	32	7	290	Activo
9	13	7	0	Activo
11	15	7	0	Activo
14	18	7	0	Activo
23	27	7	0	Activo
27	31	7	0	Activo
31	13	4	200	Activo
32	27	4	200	Activo
33	18	4	200	Activo
34	15	4	50	Activo
35	31	4	100	Activo
\.


--
-- Data for Name: dispensacion_lote; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.dispensacion_lote (id, dispensacion_id, stock_actual_id, detalle_receta_id, cantidad_entregada, estado) FROM stdin;
1	1	7	1	10	Entregado
2	2	8	2	2	Entregado
3	2	19	3	30	Entregado
4	2	18	4	15	Entregado
5	3	28	5	10	Entregado
6	3	21	6	60	Entregado
\.


--
-- Data for Name: tipos_movimiento; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.tipos_movimiento (id, descripcion, es_suma, estado) FROM stdin;
1	Devolución	t	Activo
2	Ajuste de Inventario	t	Activo
3	Traslado	t	Activo
4	Receta	t	Activo
5	Ingreso	t	Activo
6	Reubicacion	f	Activo
\.


--
-- Data for Name: movimientos; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.movimientos (id, codigo, stock_actual_id, tipo_movimiento_id, cantidad, entidad_referencia_id, fecha, observaciones, estado) FROM stdin;
2	MOV-260501-5HIO	7	5	1000	1	2026-05-01	\N	Activo
3	MOV-260501-V0SP	8	5	500	1	2026-05-01	\N	Activo
4	MOV-260501-YNA7	9	5	200	1	2026-05-01	\N	Activo
5	MOV-260501-D321	10	5	150	2	2026-05-01	\N	Activo
6	MOV-260501-E6BV	11	5	50	3	2026-05-01	\N	Activo
7	MOV-260505-6X8R	12	5	1000	4	2026-05-05	\N	Activo
8	MOV-260505-W20Y	13	5	500	4	2026-05-05	\N	Activo
9	MOV-260505-CLAX	14	5	200	4	2026-05-05	\N	Activo
10	MOV-260505-1PYG	15	5	400	4	2026-05-05	\N	Activo
11	MOV-260505-CB2Q	16	5	800	4	2026-05-05	\N	Activo
12	MOV-260505-K5OI	17	5	300	4	2026-05-05	\N	Activo
13	MOV-260505-RAND	18	5	1000	4	2026-05-05	\N	Activo
14	MOV-260505-VE9F	19	5	600	4	2026-05-05	\N	Activo
15	MOV-260505-TITD	20	5	500	4	2026-05-05	\N	Activo
16	MOV-260505-IVMW	21	5	700	4	2026-05-05	\N	Activo
17	MOV-260505-3I6W	22	5	150	4	2026-05-05	\N	Activo
18	MOV-260505-1677	23	5	200	4	2026-05-05	\N	Activo
19	MOV-260505-6ECS	24	5	400	4	2026-05-05	\N	Activo
20	MOV-260505-NM6Z	25	5	350	4	2026-05-05	\N	Activo
21	MOV-260505-QI7P	26	5	500	4	2026-05-05	\N	Activo
22	MOV-260505-US7A	27	5	100	4	2026-05-05	\N	Activo
23	MOV-260505-UEUO	28	5	300	4	2026-05-05	\N	Activo
24	MOV-260505-TF8M	29	5	450	4	2026-05-05	\N	Activo
25	MOV-260505-I3UD	7	4	10	1	2026-05-05	\N	Activo
26	MOV-260505-8DXW	8	4	2	2	2026-05-05	\N	Activo
27	MOV-260505-POKI	19	4	30	2	2026-05-05	\N	Activo
28	MOV-260505-6VAU	18	4	15	2	2026-05-05	\N	Activo
29	MOV-260505-NMX6	28	4	10	3	2026-05-05	Salida por receta RCT-260505-0F7S - Recogido por Enf. ENF-001	Activo
30	MOV-260505-AOLC	21	4	60	3	2026-05-05	Salida por receta RCT-260505-0F7S - Recogido por Enf. ENF-001	Activo
44	MOV-260505-L7K3	9	6	200	\N	2026-05-05	\N	Vaciado por Reubicación
45	MOV-260505-QR7D	23	6	200	\N	2026-05-05	\N	Vaciado por Reubicación
46	MOV-260505-C8IK	14	6	200	\N	2026-05-05	\N	Vaciado por Reubicación
47	MOV-260505-N2XE	11	6	50	\N	2026-05-05	\N	Vaciado por Reubicación
48	MOV-260505-FFAG	27	6	100	\N	2026-05-05	\N	Vaciado por Reubicación
49	MOV-260505-4UZK	31	6	200	\N	2026-05-05	\N	Consolidación por Reubicación
50	MOV-260505-8VNW	32	6	200	\N	2026-05-05	\N	Consolidación por Reubicación
51	MOV-260505-T17A	33	6	200	\N	2026-05-05	\N	Consolidación por Reubicación
52	MOV-260505-2U6K	34	6	50	\N	2026-05-05	\N	Consolidación por Reubicación
53	MOV-260505-OPQF	35	6	100	\N	2026-05-05	\N	Consolidación por Reubicación
\.


--
-- Data for Name: posologias; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.posologias (id, codigo, detalle_receta_id, dosis, unidad_medida, via_administracion, frecuencia, frecuencia_valor, duracion, indicaciones_adicionales, estado) FROM stdin;
1	POS-SH7E6Q	1	1	Tableta	Oral	Horas	8	3 días	Tomar después de las comidas si hay dolor	Activo
2	POS-T2Z16Z	2	5	ML	Oral	Horas	12	7 días	Agitar antes de usar. Mantener en refrigeración.	Activo
3	POS-FDKTA6	3	1	Tableta	Oral	Días	1	1 mes	Tomar en el desayuno.	Activo
4	POS-TMNQ5E	4	1	Tableta	Oral	Horas	8	5 días	Solo en caso de inflamación severa.	Activo
5	POS-7NY2H2	5	0.5	Tableta	Oral	Horas	24	10 días	Tomar antes de dormir. No conducir vehículos.	Activo
6	POS-WBAPZD	6	1	Cápsula	Oral	Horas	12	2 meses	Controlar presión arterial diariamente.	Activo
\.


--
-- Name: detalle_recepcion_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.detalle_recepcion_id_seq', 24, true);


--
-- Name: detalle_receta_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.detalle_receta_id_seq', 6, true);


--
-- Name: dispensacion_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.dispensacion_id_seq', 3, true);


--
-- Name: dispensacion_lote_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.dispensacion_lote_id_seq', 6, true);


--
-- Name: formas_farmaceuticas_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.formas_farmaceuticas_id_seq', 5, true);


--
-- Name: lotes_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.lotes_id_seq', 33, true);


--
-- Name: medicamentos_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.medicamentos_id_seq', 18, true);


--
-- Name: movimientos_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.movimientos_id_seq', 53, true);


--
-- Name: posologias_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.posologias_id_seq', 6, true);


--
-- Name: recepciones_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.recepciones_id_seq', 4, true);


--
-- Name: recetas_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.recetas_id_seq', 3, true);


--
-- Name: stock_actual_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.stock_actual_id_seq', 35, true);


--
-- Name: tipos_medicamentos_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.tipos_medicamentos_id_seq', 15, true);


--
-- Name: tipos_movimiento_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.tipos_movimiento_id_seq', 6, true);


--
-- Name: tipos_unidad_medida_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.tipos_unidad_medida_id_seq', 5, true);


--
-- Name: ubicaciones_almacen_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.ubicaciones_almacen_id_seq', 7, true);


--
-- PostgreSQL database dump complete
--

\unrestrict 9WR2OsmPmzsODWE8CfvXaXIwwkOTTZpym5p8bWNPPA6ymOF51dbM7WYJrgC2Sr8

