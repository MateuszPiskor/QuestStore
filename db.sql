PGDMP          :                x        
   queststore    12.2    12.2 A    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    24651 
   queststore    DATABASE     h   CREATE DATABASE queststore WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'C' LC_CTYPE = 'C';
    DROP DATABASE queststore;
                agnieszkachruszczyksilva    false            �            1259    24741 	   artifacts    TABLE     �   CREATE TABLE public.artifacts (
    id integer NOT NULL,
    name text,
    description text,
    price integer,
    type text
);
    DROP TABLE public.artifacts;
       public         heap    agnieszkachruszczyksilva    false            �            1259    24779    artifacts_id_seq    SEQUENCE     �   ALTER TABLE public.artifacts ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.artifacts_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          agnieszkachruszczyksilva    false    209            �            1259    24692    classes    TABLE     W   CREATE TABLE public.classes (
    id integer NOT NULL,
    name text,
    city text
);
    DROP TABLE public.classes;
       public         heap    agnieszkachruszczyksilva    false            �            1259    24783    classes_id_seq    SEQUENCE     �   ALTER TABLE public.classes ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.classes_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          agnieszkachruszczyksilva    false    205            �            1259    24679 
   exp_levels    TABLE     c   CREATE TABLE public.exp_levels (
    id integer NOT NULL,
    name text,
    min_points integer
);
    DROP TABLE public.exp_levels;
       public         heap    agnieszkachruszczyksilva    false            �            1259    24781    exp_levels_id_seq    SEQUENCE     �   ALTER TABLE public.exp_levels ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.exp_levels_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          agnieszkachruszczyksilva    false    204            �            1259    24718    mentor_class    TABLE     i   CREATE TABLE public.mentor_class (
    id integer NOT NULL,
    user_id integer,
    class_id integer
);
     DROP TABLE public.mentor_class;
       public         heap    agnieszkachruszczyksilva    false            �            1259    24785    mentor_class_id_seq    SEQUENCE     �   ALTER TABLE public.mentor_class ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.mentor_class_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          agnieszkachruszczyksilva    false    207            �            1259    24733    quests    TABLE        CREATE TABLE public.quests (
    id integer NOT NULL,
    name text,
    description text,
    value integer,
    type text
);
    DROP TABLE public.quests;
       public         heap    agnieszkachruszczyksilva    false            �            1259    24787    quests_id_seq    SEQUENCE     �   ALTER TABLE public.quests ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.quests_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          agnieszkachruszczyksilva    false    208            �            1259    24764    student_artifact    TABLE     s   CREATE TABLE public.student_artifact (
    id integer NOT NULL,
    student_id integer,
    artifact_id integer
);
 $   DROP TABLE public.student_artifact;
       public         heap    agnieszkachruszczyksilva    false            �            1259    24789    student_artifact_id_seq    SEQUENCE     �   ALTER TABLE public.student_artifact ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.student_artifact_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          agnieszkachruszczyksilva    false    211            �            1259    24749    student_quest    TABLE     m   CREATE TABLE public.student_quest (
    id integer NOT NULL,
    student_id integer,
    quest_id integer
);
 !   DROP TABLE public.student_quest;
       public         heap    postgres    false            �            1259    24791    student_quest_id_seq    SEQUENCE     �   ALTER TABLE public.student_quest ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.student_quest_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    210            �            1259    24660    students    TABLE     �   CREATE TABLE public.students (
    id integer NOT NULL,
    class_id integer,
    team_id integer,
    coolcoins integer,
    exp_level_id integer,
    language text
);
    DROP TABLE public.students;
       public         heap    agnieszkachruszczyksilva    false            �            1259    24793    students_id_seq    SEQUENCE     �   ALTER TABLE public.students ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.students_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          agnieszkachruszczyksilva    false    203            �            1259    24705    teams    TABLE     F   CREATE TABLE public.teams (
    id integer NOT NULL,
    name text
);
    DROP TABLE public.teams;
       public         heap    agnieszkachruszczyksilva    false            �            1259    24795    teams_id_seq    SEQUENCE     �   ALTER TABLE public.teams ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.teams_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          agnieszkachruszczyksilva    false    206            �            1259    24652    users    TABLE     �   CREATE TABLE public.users (
    id integer NOT NULL,
    name text,
    surname text,
    email text,
    phone text,
    address text,
    login json,
    password json,
    is_admin boolean,
    is_mentor boolean,
    student_id integer
);
    DROP TABLE public.users;
       public         heap    agnieszkachruszczyksilva    false            �            1259    24797    users_id_seq    SEQUENCE     �   ALTER TABLE public.users ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.users_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          agnieszkachruszczyksilva    false    202            �          0    24741 	   artifacts 
   TABLE DATA           G   COPY public.artifacts (id, name, description, price, type) FROM stdin;
    public          agnieszkachruszczyksilva    false    209   �M       �          0    24692    classes 
   TABLE DATA           1   COPY public.classes (id, name, city) FROM stdin;
    public          agnieszkachruszczyksilva    false    205   N       �          0    24679 
   exp_levels 
   TABLE DATA           :   COPY public.exp_levels (id, name, min_points) FROM stdin;
    public          agnieszkachruszczyksilva    false    204   QN       �          0    24718    mentor_class 
   TABLE DATA           =   COPY public.mentor_class (id, user_id, class_id) FROM stdin;
    public          agnieszkachruszczyksilva    false    207   {N       �          0    24733    quests 
   TABLE DATA           D   COPY public.quests (id, name, description, value, type) FROM stdin;
    public          agnieszkachruszczyksilva    false    208   �N       �          0    24764    student_artifact 
   TABLE DATA           G   COPY public.student_artifact (id, student_id, artifact_id) FROM stdin;
    public          agnieszkachruszczyksilva    false    211   9O       �          0    24749    student_quest 
   TABLE DATA           A   COPY public.student_quest (id, student_id, quest_id) FROM stdin;
    public          postgres    false    210   VO       �          0    24660    students 
   TABLE DATA           \   COPY public.students (id, class_id, team_id, coolcoins, exp_level_id, language) FROM stdin;
    public          agnieszkachruszczyksilva    false    203   sO       �          0    24705    teams 
   TABLE DATA           )   COPY public.teams (id, name) FROM stdin;
    public          agnieszkachruszczyksilva    false    206   �O       �          0    24652    users 
   TABLE DATA           {   COPY public.users (id, name, surname, email, phone, address, login, password, is_admin, is_mentor, student_id) FROM stdin;
    public          agnieszkachruszczyksilva    false    202   �O       �           0    0    artifacts_id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public.artifacts_id_seq', 1, true);
          public          agnieszkachruszczyksilva    false    212            �           0    0    classes_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('public.classes_id_seq', 4, true);
          public          agnieszkachruszczyksilva    false    214            �           0    0    exp_levels_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.exp_levels_id_seq', 1, true);
          public          agnieszkachruszczyksilva    false    213            �           0    0    mentor_class_id_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public.mentor_class_id_seq', 3, true);
          public          agnieszkachruszczyksilva    false    215            �           0    0    quests_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('public.quests_id_seq', 3, true);
          public          agnieszkachruszczyksilva    false    216            �           0    0    student_artifact_id_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('public.student_artifact_id_seq', 1, false);
          public          agnieszkachruszczyksilva    false    217            �           0    0    student_quest_id_seq    SEQUENCE SET     C   SELECT pg_catalog.setval('public.student_quest_id_seq', 1, false);
          public          postgres    false    218            �           0    0    students_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.students_id_seq', 9, true);
          public          agnieszkachruszczyksilva    false    219            �           0    0    teams_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('public.teams_id_seq', 1, false);
          public          agnieszkachruszczyksilva    false    220            �           0    0    users_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('public.users_id_seq', 13, true);
          public          agnieszkachruszczyksilva    false    221                       2606    24748    artifacts artifacts_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.artifacts
    ADD CONSTRAINT artifacts_pkey PRIMARY KEY (id);
 B   ALTER TABLE ONLY public.artifacts DROP CONSTRAINT artifacts_pkey;
       public            agnieszkachruszczyksilva    false    209                       2606    24699    classes classes_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.classes
    ADD CONSTRAINT classes_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.classes DROP CONSTRAINT classes_pkey;
       public            agnieszkachruszczyksilva    false    205                       2606    24686    exp_levels exp_levels_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.exp_levels
    ADD CONSTRAINT exp_levels_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.exp_levels DROP CONSTRAINT exp_levels_pkey;
       public            agnieszkachruszczyksilva    false    204                       2606    24722    mentor_class mentor_class_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.mentor_class
    ADD CONSTRAINT mentor_class_pkey PRIMARY KEY (id);
 H   ALTER TABLE ONLY public.mentor_class DROP CONSTRAINT mentor_class_pkey;
       public            agnieszkachruszczyksilva    false    207            	           2606    24740    quests quests_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.quests
    ADD CONSTRAINT quests_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.quests DROP CONSTRAINT quests_pkey;
       public            agnieszkachruszczyksilva    false    208                       2606    24768 &   student_artifact student_artifact_pkey 
   CONSTRAINT     d   ALTER TABLE ONLY public.student_artifact
    ADD CONSTRAINT student_artifact_pkey PRIMARY KEY (id);
 P   ALTER TABLE ONLY public.student_artifact DROP CONSTRAINT student_artifact_pkey;
       public            agnieszkachruszczyksilva    false    211                       2606    24753     student_quest student_quest_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY public.student_quest
    ADD CONSTRAINT student_quest_pkey PRIMARY KEY (id);
 J   ALTER TABLE ONLY public.student_quest DROP CONSTRAINT student_quest_pkey;
       public            postgres    false    210            �           2606    24667    students students_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.students
    ADD CONSTRAINT students_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.students DROP CONSTRAINT students_pkey;
       public            agnieszkachruszczyksilva    false    203                       2606    24712    teams teams_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.teams
    ADD CONSTRAINT teams_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.teams DROP CONSTRAINT teams_pkey;
       public            agnieszkachruszczyksilva    false    206            �           2606    24659    users users_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.users DROP CONSTRAINT users_pkey;
       public            agnieszkachruszczyksilva    false    202            �           1259    24673    fki_student_id    INDEX     F   CREATE INDEX fki_student_id ON public.users USING btree (student_id);
 "   DROP INDEX public.fki_student_id;
       public            agnieszkachruszczyksilva    false    202                       2606    24728 '   mentor_class mentor_class_class_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.mentor_class
    ADD CONSTRAINT mentor_class_class_id_fkey FOREIGN KEY (class_id) REFERENCES public.classes(id) NOT VALID;
 Q   ALTER TABLE ONLY public.mentor_class DROP CONSTRAINT mentor_class_class_id_fkey;
       public          agnieszkachruszczyksilva    false    205    207    3075                       2606    24723 &   mentor_class mentor_class_user_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.mentor_class
    ADD CONSTRAINT mentor_class_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(id) NOT VALID;
 P   ALTER TABLE ONLY public.mentor_class DROP CONSTRAINT mentor_class_user_id_fkey;
       public          agnieszkachruszczyksilva    false    202    207    3069                       2606    24774 2   student_artifact student_artifact_artifact_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.student_artifact
    ADD CONSTRAINT student_artifact_artifact_id_fkey FOREIGN KEY (artifact_id) REFERENCES public.artifacts(id) NOT VALID;
 \   ALTER TABLE ONLY public.student_artifact DROP CONSTRAINT student_artifact_artifact_id_fkey;
       public          agnieszkachruszczyksilva    false    3083    211    209                       2606    24769 1   student_artifact student_artifact_student_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.student_artifact
    ADD CONSTRAINT student_artifact_student_id_fkey FOREIGN KEY (student_id) REFERENCES public.students(id) NOT VALID;
 [   ALTER TABLE ONLY public.student_artifact DROP CONSTRAINT student_artifact_student_id_fkey;
       public          agnieszkachruszczyksilva    false    3071    203    211                       2606    24759 )   student_quest student_quest_quest_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.student_quest
    ADD CONSTRAINT student_quest_quest_id_fkey FOREIGN KEY (quest_id) REFERENCES public.quests(id) NOT VALID;
 S   ALTER TABLE ONLY public.student_quest DROP CONSTRAINT student_quest_quest_id_fkey;
       public          postgres    false    210    3081    208                       2606    24754 +   student_quest student_quest_student_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.student_quest
    ADD CONSTRAINT student_quest_student_id_fkey FOREIGN KEY (student_id) REFERENCES public.students(id) NOT VALID;
 U   ALTER TABLE ONLY public.student_quest DROP CONSTRAINT student_quest_student_id_fkey;
       public          postgres    false    210    203    3071                       2606    24700    students students_class_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.students
    ADD CONSTRAINT students_class_id_fkey FOREIGN KEY (class_id) REFERENCES public.classes(id) NOT VALID;
 I   ALTER TABLE ONLY public.students DROP CONSTRAINT students_class_id_fkey;
       public          agnieszkachruszczyksilva    false    3075    203    205                       2606    24687 #   students students_exp_level_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.students
    ADD CONSTRAINT students_exp_level_id_fkey FOREIGN KEY (exp_level_id) REFERENCES public.exp_levels(id) NOT VALID;
 M   ALTER TABLE ONLY public.students DROP CONSTRAINT students_exp_level_id_fkey;
       public          agnieszkachruszczyksilva    false    3073    203    204                       2606    24713    students students_team_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.students
    ADD CONSTRAINT students_team_id_fkey FOREIGN KEY (team_id) REFERENCES public.teams(id) NOT VALID;
 H   ALTER TABLE ONLY public.students DROP CONSTRAINT students_team_id_fkey;
       public          agnieszkachruszczyksilva    false    203    3077    206                       2606    24674    users users_student_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_student_id_fkey FOREIGN KEY (student_id) REFERENCES public.students(id) NOT VALID;
 E   ALTER TABLE ONLY public.users DROP CONSTRAINT users_student_id_fkey;
       public          agnieszkachruszczyksilva    false    202    3071    203            �   8   x�3�(�,K,IU�M�+�/��K�QH�/R0��45���K�,�L)M������ ��2      �   4   x�3����OM�N�K�54020���.J�>����E���(������ V��      �      x�3�LJM���K-�4����� 1�q      �      x�3��4�2���@�	W� #�      �   �   x�u�;1Dk�>ʂVԀ�hi�`m�*���4�hg�|&��j�	/�ú�$6�0$��G���3i
n��XKez��9��ʘ-2��"�[Uǌ������_��p��̊���������c���'=A      �      x������ � �      �      x������ � �      �   G   x�3�4���425 Q��\&P��WbY"�)�	�m�$dQ�d���,����@k�������� *�      �      x������ � �      �   s  x�u��J�0Ư���h���&�TTd7����i�H:�z���5�c��i�aAG��|�|�I<v�7e��
�� 5 ��'\�K��c/��0r\���m͡�u�%X��5SP�wk�x��}.���B�t�t�����_���J�җ��,�ꎥ���5��N�>m���<2{[`sP��Y�ʪ�������k�#�S���J֚����¿��J��E��������@ɽ�?�p�=�<�5�ä���|�M֞)W�BQT9����-��aO%���A�Ulُ��qj��09l��G݊�]!��%�l@��p�=BC&�Q�w�-����;
�s�x4-����u��5��s�i�˻     