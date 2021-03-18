node {    
      def app   
      stage('Clone repository') {                  
            checkout scm    
      }   
      stage('Build & Push') {
            sh "docker-compose build -t localhost:5000/app:${BUILD_NUMBER}"
            //sh "docker build -t localhost:5000/back:${BUILD_NUMBER} ./back"
            sh "docker-compose push localhost:5000/back:${BUILD_NUMBER}"
      }
      stage('Test') {       
            echo 'Zamislicemo da se neko testiranje desilo u nedostatku vremena i lose organizovanosti sa kolegama (sa faksa).'
       }           
      stage('Deploy') {     
            sh "docker-compose run localhost:5000/back:${BUILD_NUMBER}"       
            echo 'Done'
      }
}


      //'sh docker-compose build'
      //"sh docker build -t localhost:5000/back:latest -f ./back"

// docker.withRegistry('http://localhost:5000') {
//                   dir(./back){
//                   def customBack = docker.build("localhost:5000/back:${BUILD_NUMBER}")
//                    customBack.push();
//                   }
//                    def customFront = docker.build("docker build -t front:${BUILD_NUMBER} ./front");
                  
//                    customFront.push();
//                 }
//       }        
// pipeline {
//         agent any
//         stages {
//             stage('Build & Push images') {
//                 steps {
//                   echo 'Starting to build images.'
//                   docker.withRegistry('http://localhost:5000') {
//                   def customImage = docker.build("repository/back:${BUILD_VERSION} -f ./back")
//                   customImage.push "${env.BUILD_VERSION}"
//                   }
//                      dir ('./back') {
//                         sh 'docker build --name = back -t latest .'
//                         // sh 'docker run $DOCKER_IMAGE_NAME_2'
//                         }

//                     }
//                 }
//             }
//         }
//     }
     // "sh docker push localhost:5000/back:latest"
      // def customBack = docker.build("back:${BUILD_NUMBER} -f ./back");
      // def customFront = docker.build("front:${BUILD_NUMBER} -f ./front");
//    # Creating and running the first one
//    dir ('/path/to/your/directory2') {
//       sh 'docker build --<docker-options> -t $DOCKER_IMAGE_NAME_2 .'
//       sh 'docker run $DOCKER_IMAGE_NAME_2'

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