import axios from 'axios'

const BASE_URL = 'https://localhost:7238'

export const personApi = {
    
    async getAllPersons() {
        return await axios({
            method: 'get',
            url: `${BASE_URL}/AllPersons`,
        }).then(response => {
            return response.data         
        }).catch(e => {
            console.log(e);
        })
    },
    async insertPersonInfo(personObj) {
        return await axios({
            method: 'post',
            url: `${BASE_URL}/InsertPerson`,
            data: personObj
        }).then(response => {
            return response.data         
        }).catch(e => {
            console.log(e);
        })
    },
    async getAllAccessLogs(id) {
        return await axios({
            method: 'get',
            url: `${BASE_URL}/AllAccessLogs/${id}`,
            data: Number(id)
        }).then(response => {
            return response.data         
        }).catch(e => {
            console.log(e);
        })
    },
    async getAllRequestLogs(id) {
        return await axios({
            method: 'get',
            url: `${BASE_URL}/AllRequestLogs`,
            data: id
        }).then(response => {
            return response.data         
        }).catch(e => {
            console.log(e);
        })
    },
    async updatePersonInfo(personSelectedObj) {
        return await axios({
            method: 'put',
            url: `${BASE_URL}/UpdatePerson`,
            data: personSelectedObj
        }).then(response => {
            return response.data
        }).catch(e => {
            console.log(e);
        })
    },
    async deletePerson(personSelectedObjID) {
       return await axios({
        method: 'delete',
        url:    `${BASE_URL}/DeletePerson/` + personSelectedObjID,
        data:   personSelectedObjID
        }).then(response => {
        return response.data
        }).catch(e => {
        console.log(e);
        })
    }
}

personApi
