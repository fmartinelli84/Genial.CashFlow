FROM datalust/seq

RUN apt-get update 
RUN apt-get --yes install curl
HEALTHCHECK --interval=10s --timeout=20s --start-period=10s --retries=10 \
  CMD curl "http://localhost/api" --fail --show-error --silent