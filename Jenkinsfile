pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
		  sh "docker-composer build"
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying....'
            }
        }
    }
}
