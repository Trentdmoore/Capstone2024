<template>
    <v-container class="mt-16">
        <v-row>
            <!--Table View-->
            <v-col cols="12" fixed>
                <v-data-table :style="'text-align: left'"
                    :headers = "CurrentTableHeaders" 
                    :items = "personList"
                    :row-props="colorSelectedRow"
                    v-model="selectedRow"
                    return-object
                    select-strategy="single"
                    item-value="id"
                    :show-select="showSelected">
                
                    <template v-slot:top>
                        <v-toolbar
                            :color="'teal-lighten-1'"
                            :rounded="true"
                            dark
                        >
                            <v-toolbar-title :style="'text-align: left'">{{ currentTableView }}</v-toolbar-title>

                            <v-spacer></v-spacer>
                        </v-toolbar>
                    </template>
                </v-data-table>


                <v-alert v-model="successAlert" type="success" :text="alertText" closable class="mt-16">
                </v-alert>
            </v-col>
            
            <!--Info Form-->
            <v-col v-show="selectedRow.length > 0" id="PersonInfo" fixed>
                <v-card outlined color="grey-lighten-4" class="section-container" elevation="2" flex>
                    <v-card-title :style="'text-align: left'">Information</v-card-title>
                        <v-card-text>
                            <v-form>
                                <v-row >
                                    <v-col cols="2">
                                        <!-- Placeholder for picture -->
                                        <img src="placeholder.jpg" alt="Placeholder" class="picture"/>
                                    </v-col>
                                    <v-row>
                                        <!-- Text boxes -->
                                        <v-col cols="3">
                                            <v-text-field id="fName" variant="underlined" color="green" @update="update(id)" label="First Name" v-model="personSelectedObj.fName"></v-text-field>
                                            <v-text-field readonly="selectedRow === personSelectedObj.id" color="red" variant="underlined" label="ID" v-model="personSelectedObj.id " ></v-text-field>
                                            <v-text-field variant="underlined"  color="green" v-model="personSelectedObj.accessCode"></v-text-field>
                                        </v-col>
                                        <v-col cols="3">
                                            <v-text-field variant="underlined" color="green" label="Last Name" v-model="personSelectedObj.lName"></v-text-field>
                                            <v-text-field variant="underlined" color="green" label="CID" v-model="personSelectedObj.cid"></v-text-field>
                                        </v-col>
                                        <v-col cols="3">
                                            <v-text-field variant="underlined" color="green" label="Email" v-model="personSelectedObj.email"></v-text-field>
                                            <v-text-field variant="underlined" color="green" label="Title" v-model="personSelectedObj.title"></v-text-field>
                                        </v-col>
                                    </v-row>
                                </v-row>
                            </v-form>
                        </v-card-text>
                    <v-card-actions>
                    <!-- Save and Delete buttons -->
                    <v-btn @click="UpdatePersonInfo()" color="primary">Save</v-btn>
                    <v-btn @click="DeletePerson()" color="error">Delete</v-btn>
                    </v-card-actions>
                </v-card>
            </v-col>
        </v-row>
    </v-container>

</template>

<script>
import {personApi} from '../service/person.api.js'

    export default {
        data() {
            return {
                //Table Headers
                CurrentTableHeaders: [],
                PersonTableHeaders: [
                    {title: "ID", key: "id"},
                    {title: "First Name", key: "fName"},
                    {title: "Last Name", key: "lName"},
                    {title: "Email", key: "email"},
                    {title: "Title", key: "title"},
                    {title: "CID", key: "cid"},
                    {title: "Access", key: "accessCode"},
                ],
                RequestLogTableHeaders: [
                    {title: "ID", key: "id"},
                    {title: "Lock", key: "lockNum"},
                    {title: "Access", key: "accessCode"},
                    {title: "Date Requested", key: "dateRequested"},
                    {title: "Date Approved", key: "dateApproved"},
                    {title: "Advisor ID", key: "advisorID"},
                ],
                AccessLogTableHeaders: [
                    {title: "ID", key: "id"},
                    {title: "Lock", key: "lockNum"},
                    {title: "Time Accessed", key: "accessTime"},
                    {title: "CID", key: "cid"},
                    {title: "Accepted", key: "accepted"},
                ],
                currentTableView: "Person",
                selection: [],
                selectedRow: [],

                //DataBase Model Objects
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
                requestLogObj: {},
                accessLogObj: {},

                //Create Person Variables
                showCreatePersonDialog: false,

                //Navigation Variables


                //Alert Variables
                successAlert: false,
                alertText: "",

                //DataBase Return Lists
                accessLogList: [],
                requestLogList: [],
                personList: [],
                statisticList: [],

                showSelected: true,

                //Dropdowns for title and access on create person form
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

            async GetAllPersons() {
                await personApi.getAllPersons().then(response => {
                    this.personList = response.personList;
                    this.statisticList = response.statisticsList;
                    this.CurrentTableHeaders = this.PersonTableHeaders;
                });
            },
            //fix the table update/reload part
            async UpdatePersonInfo() {
                await personApi.updatePersonInfo(this.personSelectedObj).then(response => {
                    if(response === 'Success'){
                        this.alertText = `${this.personSelectedObj.fName} ${this.personSelectedObj.lName} was successfully updated`
                        document.getElementById("PersonInfo").style.display = "none";
                        this.successAlert = true;
                        this.setAlertTimeOut();
                        location.reload();

                    }
                    else{
                        this.alertText = `${this.personSelectedObj.fName} ${this.personSelectedObj.lName} failed to update`
                    }
                })
            },
            async DeletePerson() {
                await personApi.deletePerson(this.personSelectedObj.id).then(response => {
                    if(response === 'Success'){
                        this.alertText = `${this.personSelectedObj.fName} ${this.personSelectedObj.lName} was deleted`
                        document.getElementById("PersonInfo").style.display = "none";
                        this.successAlert =true;
                        this.setAlertTimeOut();
                        location.reload();
                    }
                    else{
                        this.alertText = `${this.personSelectedObj.fName} ${this.personSelectedObj.lName} failed to deleted`
                    }
                })
            },
            LoadTable(){
                this.GetAllPersons();
            },
            colorSelectedRow(row) {
                if(row.item === this.selectedRow[0]){
                    return {style: 'background: #42A5F5; color: white;'};
                }
            },
            toggleCreatePersonDialog(){
                if(this.showCreatePersonDialog === false){
                    this.showCreatePersonDialog = true;
                }
                else{
                    this.showCreatePersonDialog = false;
                }
            },
            setAlertTimeOut(){
                setTimeout(() =>{
                    this.successAlert=false
                }, 5000)
            }
        },
        watch: {
            selectedRow(){
                
                    this.selectedRow.forEach(item => {
                        this.personSelectedObj.id = item.id
                        this.personSelectedObj.fName = item.fName
                        this.personSelectedObj.lName = item.lName
                        this.personSelectedObj.email = item.email
                        this.personSelectedObj.title = item.title
                        this.personSelectedObj.cid = item.cid
                        this.personSelectedObj.accessCode = item.accessCode
                    })

                
            }
        },
        async beforeMount() {
            this.LoadTable();
        },
        created(){

        }
        
    }
</script>

<style scoped>

</style>