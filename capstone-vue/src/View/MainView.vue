<template>
    <v-container class="mt-16">
        <v-row>
            <!--Table View-->
            <v-col cols="12" fixed>
                <v-data-table :style="'text-align: left'"
                    :headers = "CurrentTableHeaders" 
                    :items = "personList"
                    :row-props="colorSelectedRow"
                    :item-class="colorSelectedRow"
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
                <v-card outlined color="grey-lighten-4" class="section-container" elevation="2" fixed>
                    <v-card-title :style="'text-align: left'">Information</v-card-title>
                        <v-card-text>
                            <v-form>
                                <v-row >
                                    <v-col cols="2" :style="'text-align: left'">
                                        <!-- Placeholder for picture -->
                                        <img src="placeholder.jpg" alt="Placeholder" class="picture"/>
                                    </v-col>
                                    <v-col >
                                        <v-row>
                                            <!-- Text boxes -->
                                            <v-col cols="3">
                                                <v-text-field id="fName" variant="underlined" color="green" label="First Name" v-model="personSelectedObj.fName"></v-text-field>
                                                <v-text-field readonly="selectedRow === personSelectedObj.id" color="red" variant="underlined" label="ID" v-model="personSelectedObj.id " ></v-text-field>
                                                <v-text-field variant="underlined"  color="green" label="Access" v-model="personSelectedObj.accessCode"></v-text-field>
                                                <v-text-field variant="underlined" color="green" label="Email" v-model="personSelectedObj.email"></v-text-field>
                                            </v-col>
                                            <v-col cols="3">
                                                <v-text-field variant="underlined" color="green" label="Last Name" v-model="personSelectedObj.lName"></v-text-field>
                                                <v-text-field variant="underlined" color="green" label="CID" v-model="personSelectedObj.cid"></v-text-field>
                                                <v-text-field readonly variant="underlined" color="green" label="Title" v-model="personSelectedObj.title"></v-text-field>
                                            </v-col>
                                            <v-col cols="3" :style="'text-align: center'">
                                            </v-col>
                                            <v-col cols="3" :style="'text-align: right'">
                                                    <!-- <br/>
                                                    <v-label :text="personQuickStatistics.startTime"></v-label><br /><br />
                                                    <v-label :text="personQuickStatistics.endTime"></v-label><br /><br />
                                                    <v-label :text="personQuickStatistics.numAccessesLastHour"></v-label> 
                                                    <v-label :text="personQuickStatistics.numFailedLastDay"></v-label><br /><br />
                                                    <v-label :text="personQuickStatistics.hasPendingRequest"></v-label><br /><br /> -->
                                                <v-text-field variant="underlined" disabled label="Start Time" v-model="personQuickStatistics.startTime"></v-text-field>
                                                <v-text-field variant="underlined" disabled label="EndTime" v-model="personQuickStatistics.endTime"></v-text-field>
                                                <v-text-field variant="underlined" disabled label="Access Attempts" v-model="personQuickStatistics.numAccessesLastHour"></v-text-field>
                                                <v-text-field variant="underlined" disabled label="Failed Attempts" v-model="personQuickStatistics.numFailedLastDay"></v-text-field>
                                                <v-text-field variant="underlined" disabled label="Pending Request" v-model="personQuickStatistics.hasPendingRequest"></v-text-field>
                                            </v-col>
                                        </v-row>
                                    </v-col>
                                </v-row>
                            </v-form>
                        </v-card-text>
                    <v-card-actions>
                    <!-- Save and Delete buttons -->
                    <v-btn @click="GoToLogsPerson()" color="primary">Logs</v-btn>
                    <v-btn @click="toggleUpdateAlertDialog()" color="success">Save</v-btn>
                    <v-btn @click="toggleDeleteAlertDialog()" color="error">Delete</v-btn>
                    </v-card-actions>
                </v-card>
            </v-col>
        </v-row>
    </v-container>

     <!--Update Alert Dialog-->
     <v-dialog max-width="500" v-model="showUpdateAlert">
        <template v-slot:default="{isActive}">
             <v-card title="Save Updates">
                <v-card-text>
                    Are you sure you want to save these changes?
                </v-card-text>

                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn text="Save" @click="UpdatePersonInfo()" id="saveButton"></v-btn>
                    <v-btn text="Cancel" @click="isActive.value = false" id="cancelButton"></v-btn>
                </v-card-actions>
            </v-card>
            </template>
    </v-dialog>

    <!--Delete Alert Dialog-->
    <v-dialog max-width="500" v-model="showDeleteAlert">
        <template v-slot:default="{isActive}">
             <v-card title="Delete Person">
                <v-card-text>
                    Are you sure you want to delete this person?
                </v-card-text>

                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn text="Delete" @click="DeletePerson()" id="deleteButton"></v-btn>
                    <v-btn text="Cancel" @click="isActive.value = false" id="cancelButton"></v-btn>
                </v-card-actions>
            </v-card>
            </template>
    </v-dialog>
</template>

<script>
import router from '@/router';
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
                currentTableView: "Person",
                selection: [],
                selectedRow: [],
                rowItem: {},

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
                personQuickStatistics: {
                    id: 0,
                    startTime: "",
                    endTime: "",
                    numFailedLastDay: 0,
                    hasPendingRequest: "",
                    numAccessesLastHour: 0
                },

                //Create Person Variables
                showCreatePersonDialog: false,

                //Alert Variables
                successAlert: false,
                alertText: "",
                showUpdateAlert: false,
                showDeleteAlert: false,

                //DataBase Return Lists
                personList: [],
                statisticList: [],

                showSelected: true,
            
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
                        this.showUpdateAlert = false;
                        this.alertText = `${this.personSelectedObj.fName} ${this.personSelectedObj.lName} was successfully updated`
                        this.successAlert = true;
                        this.setAlertTimeOut();
                        this.ReloadTableOnChange();

                    }
                    else{
                        this.alertText = `${this.personSelectedObj.fName} ${this.personSelectedObj.lName} failed to update`
                    }
                })
            },
            async DeletePerson() {
                await personApi.deletePerson(this.personSelectedObj.id).then(response => {
                    if(response === 'Success'){
                        this.showDeleteAlert = false;
                        this.alertText = `${this.personSelectedObj.fName} ${this.personSelectedObj.lName} was deleted`
                        document.getElementById("PersonInfo").style.display = 'none'
                        this.successAlert =true;
                        this.setAlertTimeOut();
                        this.ReloadTableOnChange();
                    }
                    else{
                        this.alertText = `${this.personSelectedObj.fName} ${this.personSelectedObj.lName} failed to deleted`
                    }
                })
            },
            LoadTable(){
                this.GetAllPersons();
            },
            ReloadTableOnChange(){
                this.GetAllPersons();
                this.selectedRow = [];
                this.selectedRow.push(this.personSelectedObj);
                this.colorSelectedRow(this.selectedRow.push(this.personSelectedObj));
                location.reload();
            },
            GoToLogsPerson(){
                router.push(`/LogInfo/${this.personSelectedObj.id}`)
            },
            colorSelectedRow(row) {          
                if(row.item === this.selectedRow[0]){
                    this.rowItem = row;
                    return {style: 'background: #42A5F5; color: white;'};             
                }
            },
            toggleUpdateAlertDialog(){
                if(this.showUpdateAlert === false){
                    this.showUpdateAlert = true;
                }
                else{
                    this.showUpdateAlert = false;
                }
            },
            toggleDeleteAlertDialog(){
                if(this.showDeleteAlert === false){
                    this.showDeleteAlert = true;
                }
                else{
                    this.showDeleteAlert = false;
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
                    });
                    this.statisticList.forEach(stat => {
                        if(stat.id == this.personSelectedObj.id){
                            this.personQuickStatistics.id = stat.id
                            this.personQuickStatistics.startTime = stat.startTime
                            this.personQuickStatistics.endTime = stat.endTime
                            this.personQuickStatistics.numFailedLastDay = stat.numFailedLastDay
                            this.personQuickStatistics.hasPendingRequest = stat.hasPendingRequest
                            this.personQuickStatistics.numAccessesLastHour = stat.numAccessesLastHour
                        }
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