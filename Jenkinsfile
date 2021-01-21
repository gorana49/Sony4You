pipeline {
    agent none
    stages {
	 stage('Redis') {
            agent {
                docker { image 'redis:' }
            }
            steps {
                sh 'mvn --version'
            }
        }
	 stage('Neo4J') {
            agent {
                docker { image 'neo4j' }
            }
            steps {
                sh 'mvn --version'
            }
        }
        stage('Back-end') {
            agent {
                docker {  dockerfile true  }
            }
        }
        stage('Front-end') {
            agent {
                 docker {  dockerfile true  }
            }
            steps {
                sh 'node --version'
            }
        }
	stage('Load-balancer') {
            agent {
                 docker {  dockerfile true  }
            }
            steps {
                sh 'node --version'
            }
        }
    }
}
