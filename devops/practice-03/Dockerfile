FROM python:3.11
WORKDIR /build

COPY requirements.txt .
RUN pip wheel -w libs -r requirements.txt

FROM python:3.11-slim
EXPOSE 8080

RUN apt update && apt install -y curl
RUN rm -rf /var/lib/apt/lists/*

VOLUME ["/var/log/app"]
ENV APP_LOG_FILE=/var/log/app/log.txt

RUN useradd -ms /bin/bash appuser
RUN mkdir -p /var/log/app
RUN chown appuser /var/log/app

USER appuser
WORKDIR /home/appuser

HEALTHCHECK --interval=30s --timeout=5s --retries=3 CMD curl --fail http://localhost:8080/health

COPY main.py .
COPY --from=0 /build/libs libs
RUN pip install libs/*.whl

CMD ["python3", "main.py"]
