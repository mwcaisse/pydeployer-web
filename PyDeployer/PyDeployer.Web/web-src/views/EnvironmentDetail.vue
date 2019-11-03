<template>
    <div>
        <section class="section">
            <div class="container">
                <h1 class="title">Environment Details</h1>
            </div>
        </section>
        <div class="columns">
            <div class="column">
                <environment-details :environmentId="environmentId"></environment-details>
            </div>
        </div>
        <div class="columns">
            <div class="column">
                <environment-application-list :environmentId="environmentId"></environment-application-list>
            </div>
        </div>
    </div>
</template>

<script>
    import EnvironmentDetails from "@app/components/Environment/EnvironmentDetails.vue"
    import EnvironmentApplicationList from "@app/components/Environment/EnvironmentApplicationList.vue"
    
    const environmentId = parseInt($("#environmentId").val(), 10);
    
    export default {
        data: function () {
            return {
                environmentId: environmentId
            }
        },
        components: {
            EnvironmentDetails,
            EnvironmentApplicationList
        }        
    }
</script>

