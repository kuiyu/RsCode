const App = {
    data() {
        return {
            list: [
            ],
            columns: [
                {
                    colKey: 'name',
                    title: '插件名称',
                    width: 200
                },
                {
                    colKey: 'description',
                    title: '插件描述',
                },
                {
                    colKey: 'version',
                    title: '插件版本号',
                    width: 120
                },
                {
                    colKey: 'status',
                    title: '插件状态',
                    width:100
                },
                {
                    colKey: 'opt',
                    title: '操作'
                },
            ]
        }
    },
    methods: {
        getData() {
            axios.get("/plugin/all")
                .then(ret => {
                    console.log(JSON.stringify(ret))
                    this.list = ret.data
                })
        },
        open(status, plugin) {
            var pluginName=plugin.name
            
            if (status == 1) {
                axios.get("/plugin/enable?pluginName=" + pluginName)
                this.list= this.list.map((item, index) => {
                    if (item.name == pluginName) {
                       item.status=1
                    }
                    return item;
                })
            }
            if (status == 0) {
                axios.get("/plugin/disable?pluginName=" + pluginName)
                this.list = this.list.map((item, index) => {
                    if (item.name == pluginName) {
                        item.status = 0
                    }
                    return item;
                })
            }
        }
    },
    mounted() {
        this.getData()
    }
}

Vue.createApp(App).use(TDesign).mount('#app');