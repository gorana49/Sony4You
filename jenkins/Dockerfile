FROM jenkins/jenkins:2.263.4-lts-jdk11

ARG HOST_UID=1001
ARG HOST_GID=999

USER root
RUN apt-get update && apt-get install -y apt-transport-https \
 ca-certificates curl software-properties-common
RUN curl -fsSL https://download.docker.com/linux/debian/gpg | apt-key add -
RUN apt-key fingerprint 0EBFCD88
RUN add-apt-repository \
        "deb [arch=amd64] https://download.docker.com/linux/debian \
        $(lsb_release -cs) stable"
RUN apt-get update && apt-get install -y docker-ce-cli containerd.io
RUN groupadd -g ${HOST_GID} docker
RUN usermod -aG docker root 
RUN newgrp docker 
RUN curl -L https://github.com/docker/compose/releases/download/1.25.5/docker-compose-$(uname -s)-$(uname -m) -o /usr/local/bin/docker-compose && \
 chmod +x /usr/local/bin/docker-compose && \
 ln -s /usr/local/bin/docker-compose /usr/bin/docker-compose
RUN usermod -u ${HOST_UID} jenkins
RUN groupmod -g ${HOST_GID} docker
RUN usermod -aG docker jenkins
USER ${HOST_UID}