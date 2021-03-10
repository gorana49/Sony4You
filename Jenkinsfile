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
        sh "docker build ."
      }
    }

    stage("Test front end") {
      agent {
        dockerfile {
          filename "front/Dockerfile"
        }
      }

      steps {
         sh "docker build ."
      }
    }
  }
}
