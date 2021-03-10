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
        echo "docker ne radi"
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
