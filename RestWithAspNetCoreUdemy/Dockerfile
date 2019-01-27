FROM mysql:5.7.22
EXPOSE 3306
#COPY ./RestWithAspNetCoreUdemy/Db/migrations/ /home/database/
#COPY ./RestWithAspNetCoreUdemy/Db/dataset/ /home/database/
COPY /DbBackup/ /home/database/
COPY ./RestWithAspNetCoreUdemy/ci/init_database.sh /docker-entrypoint-initdb.d/init_database.sh