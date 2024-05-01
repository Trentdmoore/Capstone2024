<template>
    <!--Create Person Form-->
    <v-container class="mt-16">

        <v-toolbar  
                :color="'teal-lighten-1'"
                :rounded="true"
                dark
                >
                <v-toolbar-title :style="'text-align: left'">Select the type of person</v-toolbar-title>
                <v-spacer></v-spacer>
                <v-btn variant="outlined" :color="'white'" class="mr-3" @click="showCardSection('guest')">Guest</v-btn>
                <v-btn variant="outlined" :color="'white'" class="mr-3" @click="showCardSection('student')">Student</v-btn>
                <v-btn variant="outlined" :color="'white'" class="mr-3" @click="showCardSection('employee')">Employee</v-btn>
                <v-btn variant="outlined" :color="'white'" class="mr-3" @click="showCardSection('admin')">Admin</v-btn>
                


        </v-toolbar>

        <v-form class="mt-16">
        
            <v-alert v-model="successAlert" type="success" :text="alertText" closable class="mt-16">
            </v-alert>

            <!--guest card section-->
            <v-card width="1000" height="400" class="mx-auto" color="grey-lighten-4" v-if="showCard === true">
                <v-card-title>{{ titleType }}</v-card-title>
                <v-card-subtitle>Fill out form and click sumbit</v-card-subtitle>
                          
                <v-card-item>
                    <v-form>
                        <v-row>
                            <v-col cols = 3>
                                <v-text-field variant="underlined" label="First Name" v-model="personObj.fName"></v-text-field>
                            </v-col>
                            <v-col cols = 3>
                                <v-text-field variant="underlined" label="Last Name" v-model="personObj.lName"></v-text-field>
                            </v-col>
                            <v-col cols = 3>
                                <v-text-field variant="underlined" label="Email" v-model="personObj.email"></v-text-field>
                            </v-col>
                            <v-col cols = 3>
                                <v-select
                                        v-model="personObj.accessCode"
                                            :items="dropdownItems"
                                            label="Access"
                                            variant="underlined"
                                ></v-select>
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col cols = 3>
                                <v-text-field  id="idField" variant="underlined" label="ID" v-model="personObj.id">{{ guestID }}</v-text-field>
                            </v-col>
                            <v-col cols = 3>
                                <v-text-field  id="cidField" variant="underlined" label="CID" v-model="personObj.cid">{{ guestCID }}</v-text-field>
                            </v-col>
                            <v-col cols="3">
                                <v-text-field readonly color="error" variant="underlined"  v-model="personObj.title">{{ titleHolder }}</v-text-field>
                            </v-col>
                            <v-col cols="3">
                                <v-text-field variant="underlined" label="Department" v-if="showDepartment === true" v-model="employeeObj.department"></v-text-field>
                            </v-col>                     
                        </v-row>
        
                        <v-row>
                            <v-col></v-col>
                        </v-row>
                    </v-form>
                    <v-btn :color="'success'" class="mr-3" @click="InsertPersonInfo()">Submit</v-btn>
                </v-card-item>
            </v-card>
        </v-form>
    </v-container>
</template>

<script>
import {personApi} from '../service/person.api.js'

    export default {
        data() {
            return {
                //DataBase Model Objects
                successAlert:  false,

                personObj: {
                    id: "",
                    fName: "",
                    lName: "",
                    email: "",
                    title: "",
                    cid: "",
                    accessCode: ""
                },
                guestObj: {
                    id: "",
                    fName: "",
                    lName: "",
                    email: "",
                    title: "",
                    cid: "",
                    accessCode: ""
                },
                employeeObj: {
                    id: "",
                    fName: "",
                    lName: "",
                    email: "",
                    title: "",
                    cid: "",
                    accessCode: "",
                    department: "",

                },
                adminObj: {
                    id: "",
                    fName: "",
                    lName: "",
                    email: "",
                    title: "",
                    cid: "",
                    accessCode: ""
                },

                selectedItem: null,
                    dropdownItems: [
                        'WKD',
                        'ALL',
                        'Day'
                    ],

                    //control variables for cards
                    showCard: false,
                    titleType: "",
                    titleHolder: "",
                    guestID: "",
                    guestCID: "",
                    showDepartment: false,
            }
        },
        methods: {
            async InsertPersonInfo(){
                await personApi.insertPersonInfo(this.personObj).then(response => {
                    if(response === 'Success'){
                        this.alertText = `${this.personObj.fName} ${this.personObj.lName} was successfully created`
                        this.successAlert = true;
                        this.setAlertTimeOut();
                        location.reload();
                    }
                    else{
                        this.alertText = `${this.personObj.fName} ${this.personObj.lName} failed to created`
                        this.successAlert = true;
                        this.setAlertTimeOut();
                    }
                })
            },
            setAlertTimeOut(){
                setTimeout(() =>{
                    this.successAlert=false
                }, 1000)
            },
            async GetAllPersons() {
                await personApi.getAllPersons().then(response => {
                    this.personList = response;
                    this.CurrentTableHeaders = this.PersonTableHeaders;
                });
            },
            showCardSection(personType){
                if(personType === "guest"){
                    this.showCard = true;
                    this.titleType = 'Create Guest'
                    this.titleHolder = 'Guest'
                    this.guestID = "1234"
                    this.guestCID = "5678"
                }
                else if(personType === "student"){
                    this.showCard = true;
                    this.titleType = 'Create Student'
                    this.personObj.title = 'Student'
                }
                else if(personType === "employee"){
                    this.showCard = true;
                    this.titleType = 'Create Employee'
                    this.titleHolder = 'Employee'
                    this.showDepartment = true;
                }
                else if(personType === "admin"){
                    this.showCard = true;
                    this.titleType = 'Create Administrator'
                    this.titleHolder = 'Administrator'
                }

            }
        }
    }
</script>

<style lang="scss" scoped>

</style>