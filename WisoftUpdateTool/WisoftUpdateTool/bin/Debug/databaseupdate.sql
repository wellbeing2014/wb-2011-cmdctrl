insert into system_version_info (ID, MODULENAME, MODULECODE, VERSION, PUBLISH_DATE, UPDATE_DATE, REMARK)
select '', '电子监察子平台', 'emp', '1.102.1', '2012-07-09', to_char(sysdate,'yyyy-mm-dd hh24:mi:ss'), '电子监察子平台' from dual;