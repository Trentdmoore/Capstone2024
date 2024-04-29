<template>
    <!--Create Person Form-->
    <v-container class="ma-auto">
        <v-form class="mx-auto">

            <v-alert v-model="successAlert" type="success" :text="alertText" closable class="mt-16">
            </v-alert>

            <v-card width="1000" height="400" class="mx-auto">
                <v-card-title>Create Person</v-card-title>
                <v-card-subtitle>Fill out form and click sumbit</v-card-subtitle>
                          
                <v-card-item>
                    <v-form>
                        <v-row>
                            <v-col cols = 3>
                                <v-text-field variant="underlined" label="ID" v-model="personObj.id">
        
                                </v-text-field>
                            </v-col>
                            <v-col cols = 3>
                                <v-text-field variant="underlined" label="CID" v-model="personObj.cid">
    
                                </v-text-field>
                            </v-col>
                            
                            <v-col cols = 3>
                                <v-text-field variant="underlined" label="Email" v-model="personObj.email">
        
                                </v-text-field>
                            </v-col>
                            <v-col cols = 3>
                                
                                <v-select
                                        v-model="personObj.title"
                                            :items="dropdownItems2"
                                            label="Title"
                                            variant="underlined"
                                ></v-select>
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col cols = 3>
                                <v-text-field variant="underlined" label="First Name" v-model="personObj.fName">
        
                                </v-text-field>
                            </v-col>
                            <v-col cols = 3>
                                <v-text-field variant="underlined" label="Last Name" v-model="personObj.lName">
        
                                </v-text-field>
                            </v-col>
    
                            <v-col cols="3">
                                <v-select
                                        v-model="personObj.accessCode"
                                            :items="dropdownItems"
                                            label="Access"
                                            variant="underlined"
                                ></v-select>
                            </v-col>                    
                        </v-row>
        
                        <v-row>
                            <v-col>
        
                            </v-col>
                        </v-row>
                    </v-form>
                    <v-btn :color="'success'" class="mr-3" @click="InsertPersonInfo()">Submit</v-btn>
                    <v-btn :color="'error'" @click="toggleCreatePersonDialog()">Close</v-btn>
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

                personSelectedObj: {
                    id: "",
                    fName: "",
                    lName: "",
                    email: "",
                    title: "",
                    cid: "",
                    accessCode: ""
                },
                personObj: {
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
                    ],

                selectedItem2: null,
                    dropdownItems2: [
                        'Student',
                        'Guest'
                    ],
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
                }, 4000)
            },
            async GetAllPersons() {
                await personApi.getAllPersons().then(response => {
                    this.personList = response;
                    this.CurrentTableHeaders = this.PersonTableHeaders;
                });
            },
        }
    }
</script>

<style lang="scss" scoped>

</style>