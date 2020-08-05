PGDMP                         x         
   store_data    12.2    12.2                0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            	           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            
           1262    16393 
   store_data    DATABASE     �   CREATE DATABASE store_data WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'Russian_Russia.1251' LC_CTYPE = 'Russian_Russia.1251';
    DROP DATABASE store_data;
                postgres    false            �            1255    16517    delete_picket(integer, integer)    FUNCTION     �   CREATE FUNCTION public.delete_picket(_picket_num integer, _store_num integer) RETURNS void
    LANGUAGE plpgsql
    AS $$
      BEGIN

        DELETE FROM last_info WHERE picket_num = _picket_num AND store_num = _store_num ;

      END;

  $$;
 M   DROP FUNCTION public.delete_picket(_picket_num integer, _store_num integer);
       public          postgres    false            �            1255    16757    drop_table(text)    FUNCTION     e  CREATE FUNCTION public.drop_table(_key_word text) RETURNS void
    LANGUAGE plpgsql
    AS $$
DECLARE
    row     record;
BEGIN
    FOR row IN 
        SELECT
            table_schema,
            table_name
        FROM
            information_schema.tables
        WHERE
            table_type = 'BASE TABLE'
        AND
            table_name ILIKE (_key_word || '%')
    LOOP
        EXECUTE 'DROP TABLE ' || quote_ident(row.table_schema) || '.' || quote_ident(row.table_name);
        RAISE INFO 'Dropped table: %', quote_ident(row.table_schema) || '.' || quote_ident(row.table_name);
    END LOOP;
END;
$$;
 1   DROP FUNCTION public.drop_table(_key_word text);
       public          postgres    false            �            1255    16437 "   get_field_picket(integer, integer)    FUNCTION     �   CREATE FUNCTION public.get_field_picket(picket_name integer, room integer) RETURNS text
    LANGUAGE plpgsql
    AS $$
BEGIN 
RETURN(
	SELECT field_name FROM last_info WHERE picket_num = picket_name AND store_num = room); 

END;
$$;
 J   DROP FUNCTION public.get_field_picket(picket_name integer, room integer);
       public          postgres    false            �            1255    16550 Y   insert_picket(integer, integer, character, character, integer, integer, integer, integer)    FUNCTION     	  CREATE FUNCTION public.insert_picket(picket_num integer, store_num integer, field_name character, goods_name character, goods_weight integer, goods_volume integer, max_weight integer, max_volume integer) RETURNS void
    LANGUAGE plpgsql
    AS $$
      BEGIN

		
        INSERT INTO last_info (picket_num,store_num,field_name,goods_name,goods_weight,goods_volume,max_weight,max_volume)

        VALUES(picket_num, store_num, field_name,goods_name, goods_weight, goods_volume, max_weight, max_volume);

      END;

  $$;
 �   DROP FUNCTION public.insert_picket(picket_num integer, store_num integer, field_name character, goods_name character, goods_weight integer, goods_volume integer, max_weight integer, max_volume integer);
       public          postgres    false            �            1255    16441    picket_exsist(integer, integer)    FUNCTION     J  CREATE FUNCTION public.picket_exsist(_picket_num integer, _store_num integer, OUT exsist boolean) RETURNS boolean
    LANGUAGE plpgsql
    AS $$
BEGIN
    IF (SELECT EXISTS(SELECT picket_num FROM last_info WHERE picket_num=_picket_num AND store_num = _store_num)) THEN
		exsist := true;
	ELSE
		exsist := false;
	END IF;
END;
$$;
 a   DROP FUNCTION public.picket_exsist(_picket_num integer, _store_num integer, OUT exsist boolean);
       public          postgres    false            �            1255    16439 -   set_field_picket(integer, character, integer)    FUNCTION        CREATE FUNCTION public.set_field_picket(_picket_name integer, _new_field character, room integer) RETURNS void
    LANGUAGE plpgsql
    AS $$

      BEGIN

        
		
	UPDATE last_info SET field_name = _new_field WHERE picket_num = _picket_name AND store_num = room ;

      END;

  $$;
 a   DROP FUNCTION public.set_field_picket(_picket_name integer, _new_field character, room integer);
       public          postgres    false            �            1255    16438 4   set_field_where_field(character, character, integer)    FUNCTION     #  CREATE FUNCTION public.set_field_where_field(_old_field character, _new_field character, room integer) RETURNS void
    LANGUAGE plpgsql
    AS $$

      BEGIN

        
		
	UPDATE last_info SET field_name = _new_field WHERE field_name = _old_field AND store_num = room ;

      END;

  $$;
 f   DROP FUNCTION public.set_field_where_field(_old_field character, _new_field character, room integer);
       public          postgres    false            �            1255    16549    to_history()    FUNCTION     z  CREATE FUNCTION public.to_history() RETURNS void
    LANGUAGE plpgsql
    AS $$
DECLARE _table_name text :=  to_char(now()::timestamp, 'HH12:MI:SS:MS_DD_Mon_YYYY');
      BEGIN

		EXECUTE format('
   CREATE TABLE IF NOT EXISTS %I AS TABLE last_info', _table_name);
   		
		EXECUTE format('
   INSERT INTO %I SELECT * FROM last_info', _table_name);
		
        
      END;

  $$;
 #   DROP FUNCTION public.to_history();
       public          postgres    false            �            1255    16507 ;   update_goods(integer, integer, character, integer, integer)    FUNCTION     �  CREATE FUNCTION public.update_goods(_picket_num integer, _store_num integer, _goods_name character, _goods_volume integer, _goods_weight integer) RETURNS void
    LANGUAGE plpgsql
    AS $$
DECLARE _table_name text :=  to_char(now()::timestamp, 'HH12:MI:SS:MS_DD_Mon_YYYY');
      BEGIN

		
		
		EXECUTE format('
   CREATE TABLE IF NOT EXISTS %I AS TABLE last_info', _table_name);
   		
		EXECUTE format('
   INSERT INTO %I SELECT * FROM last_info', _table_name);

        UPDATE last_info SET goods_name = _goods_name, goods_volume = _goods_volume, goods_weight = _goods_weight
	
	WHERE picket_num = _picket_num AND store_num = _store_num ;

      END;

  $$;
 �   DROP FUNCTION public.update_goods(_picket_num integer, _store_num integer, _goods_name character, _goods_volume integer, _goods_weight integer);
       public          postgres    false            �            1259    16407 	   last_info    TABLE     �   CREATE TABLE public.last_info (
    picket_num integer,
    store_num integer,
    field_name character(10),
    goods_name character(30),
    goods_weight integer,
    goods_volume integer,
    max_weight integer,
    max_volume integer
);
    DROP TABLE public.last_info;
       public         heap    postgres    false                      0    16407 	   last_info 
   TABLE DATA           �   COPY public.last_info (picket_num, store_num, field_name, goods_name, goods_weight, goods_volume, max_weight, max_volume) FROM stdin;
    public          postgres    false    202   *          �   x���K� �5���0��z�c���ĕ1^�Q�՞a����Ap��d/�?��0�Y B�k��=�S|�%ބo�Z���:�s�cA��K}�@v����)�y����@�ڛİ
�e���'��pK�%~��H�r�5�v�뉮(�3���(;��Q&NK[��X���(iWz�u��y���ŨF��eGµ�@���d���Q�s�R��J     