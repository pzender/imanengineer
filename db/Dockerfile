FROM mcr.microsoft.com/mssql/server
RUN mkdir /sql
ADD ./sql /sql
RUN ( /opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "Service Broker manager has started" \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'P@ssw0rd' -i /sql/initial.sql \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'P@ssw0rd' -d tv_db -i /sql/create.sql \
    && pkill sqlservr 
