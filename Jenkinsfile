node {    
      def app   
      stage('Clone repository') {                  
            checkout scm    
      }   
      stage('Build & Push') {
            sh "docker-compose build" 
            sh "docker-compose push"
            //sh "docker build -t localhost:5000/back:${BUILD_NUMBER} ./back"
      }
      stage('Test') {       
            echo 'Zamislicemo da se neko testiranje desilo u nedostatku vremena i lose organizovanosti.'
       }           
      stage('Deploy') {   
            sh  "docker-compose up"       
            echo 'Done'
      }
}
// pipeline{
//       agent {
//       dockerfile {
//         filename 'Dockerfile'
//         dir './back'
//         registryUrl 'https://localhost:5000/'
//         registryCredentialsId 'JenkinsCred'} 
//       }
//       stages {
//             stage ('Build & Push') {
//                  steps {
//                        echo "Starting to build images."
//                  } 
//             }
//             stage ('Test'){
//                   steps {
//                        echo "Testing"
//                  } 
//             }
//       }
// }
            // sh "docker run redis:latest -d" 
            // sh  "docker run neo4j:3.5 -d" 