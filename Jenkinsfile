pipeline {
  agent none

  stages {
    stage("Test back end") {
      agent {
        dockerfile {
          filename "back/Dockerfile"
        }
      }

      steps {
        sh "docker-machine rm default"
        sh "docker ps"
      }
    }

    stage("Test front end") {
      agent {
        dockerfile {
          filename "front/Dockerfile"
        }
      }

      steps {
         echo 'Frontend is running'
      }
    }
  }
}
