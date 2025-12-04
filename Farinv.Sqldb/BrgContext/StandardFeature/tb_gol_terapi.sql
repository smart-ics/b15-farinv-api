CREATE TABLE tb_gol_terapi
(
    fs_kd_gol_terapi   VARCHAR(20)  NOT NULL CONSTRAINT DF_tb_gol_terapi_fs_kd_gol_terapi DEFAULT(''),
    fs_nm_gol_terapi   VARCHAR(100) NOT NULL CONSTRAINT DF_tb_gol_terapi_fs_nm_gol_terapi DEFAULT(''),
    CONSTRAINT PK_tb_gol_terapi PRIMARY KEY CLUSTERED (fs_kd_gol_terapi)
)