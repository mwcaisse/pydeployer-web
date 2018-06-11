<template>
    <div>
        <section class="section">
            <div class="container">
                <h1 class="title">Application Details</h1>
                <p class="subtitle">
                    Details of Test Application 1
                </p>
            </div>
        </section>
        <div class="columns">
            <div class="column">
                <div class="box">
                    <div class="field">
                        <label class="label">Name</label>
                        <div class="control">
                            <span>Test Application 1</span>
                        </div>
                    </div>
                    <div class="field">
                        <label class="label">UUID</label>
                        <div class="control">
                            <span>UUID GOES HERE</span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="column">
                <div class="box">
                    <h2>Environments</h2>
                    <p>A list/grid of the environments that this application is associated with</p>
                </div>
            </div>
        </div>
        <div class="box">
            <h2>Tokens</h2>
            <p>A list of all the Tokens that have been defined for this application.</p>
        </div>
    </div> 
</template>

<script>
    export default {
        data: function() {
            return {
                "message": "Welcome again"
            }
        }
    }
</script>