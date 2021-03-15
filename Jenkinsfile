node {    
      def app   
      stage('Clone repository') {               
             
            checkout scm    
      }   
      stage('Building Docker Image') {

      //'sh docker-compose build'

      "sh docker build -t localhost:5000/back:${BUILD_NUMBER} -f ./back"
      "sh docker push localhost:5000/back${BUILD_NUMBER}"
      // def customBack = docker.build("back:${BUILD_NUMBER} -f ./back");
      // def customFront = docker.build("front:${BUILD_NUMBER} -f ./front");
//    # Creating and running the first one
//    dir ('/path/to/your/directory2') {
//       sh 'docker build --<docker-options> -t $DOCKER_IMAGE_NAME_2 .'
//       sh 'docker run $DOCKER_IMAGE_NAME_2'
//    }
      }        
      stage('Test') {       
            // app = docker.build("my-image -f ./back")
            echo 'Done'
       }           
      stage('Deploy') {                       
            echo 'Done'
      }
}
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