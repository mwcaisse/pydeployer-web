<template>
    <div>
        <div class="box">
            <div class="field">
                <label class="label">Name</label>
                <div class="control">
                    <span>{{ name }}</span>
                </div>
            </div>
            <div class="field">
                <label class="label">UUID</label>
                <div class="control">
                    <span>{{ uuid }}</span>
                </div>
            </div>
            <div class="field">
                <label class="label">Host Name</label>
                <div class="control">
                    <span>{{ hostName }}</span>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import {EnvironmentService} from "@app/services/ApplicationProxy";

export default {
    name: "EnvironmentDetails",
    props: {
        environmentId: {
            type: Number,
            required: true
        }
    },
    data: function() {
        return {
            name: "",
            uuid: "",
            hostName: ""
        }
    },
    created: function () {
        this.fetchEnvironment();
    },
    methods: {
        fetchEnvironment: function () {
            EnvironmentService.get(this.environmentId).then(function (data) {
                this.update(data);
            }.bind(this), function (error) {
                console.log("Error fetching environments: " + error);
            });
        },
        update: function (data) {
            this.name = data.name;
            this.uuid = data.environmentUuid;
            this.hostName = data.hostName;
        },
        clear: function () {
            this.name = "";
            this.uuid = "";
            this.hostName = "";
        }
        
    }
}
</script>
