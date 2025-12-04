CREATE TABLE tb_kelas_terapi
(
    fs_kd_kelas_terapi   VARCHAR(20)  NOT NULL CONSTRAINT DF_tb_kelas_terapi_fs_kd_kelas_terapi DEFAULT(''),
    fs_nm_kelas_terapi   VARCHAR(100) NOT NULL CONSTRAINT DF_tb_kelas_terapi_fs_nm_kelas_terapi DEFAULT(''),
    CONSTRAINT PK_tb_kelas_terapi PRIMARY KEY CLUSTERED (fs_kd_kelas_terapi)
)