<template>
    <div>
        <section class="section">
            <div class="container">
                <h1 class="title">
                    Build Tokens
                </h1>
            </div>
        </section>
        <div class="columns">
            <div class="column">
                <build-token />
            </div>
        </div>
    </div>
</template>

<script>
import BuildToken from "@app/components/BuildToken/BuildToken.vue"

export default {
    components: {
        BuildToken
    },
    data: function() {
        return {       
        }
    }
}
</script>