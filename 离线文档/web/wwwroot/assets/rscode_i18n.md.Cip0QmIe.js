import{_ as s,c as a,o as n,a4 as p}from"./chunks/framework.CI8or701.js";const g=JSON.parse('{"title":"asp.net core项目国际化","description":"asp.net core项目国际化的实现方法,可使用json做为国际化资源的实现","frontmatter":{"home":false,"editLink":false,"lang":"zh-CN","title":"asp.net core项目国际化","description":"asp.net core项目国际化的实现方法,可使用json做为国际化资源的实现"},"headers":[],"relativePath":"rscode/i18n.md","filePath":"rscode/i18n.md","lastUpdated":1732781721000}'),i={name:"rscode/i18n.md"},e=p(`<p>增加了使用json文件做为国际化资源的实现方法，使用步骤如下：</p><h2 id="为项目创建资源文件" tabindex="-1">为项目创建资源文件 <a class="header-anchor" href="#为项目创建资源文件" aria-label="Permalink to &quot;为项目创建资源文件&quot;">​</a></h2><p>需要为使用到国际化资源的每个页面以及子页面，创建国际化资源，例:假如你的项目结构是这样</p><div class="language- vp-adaptive-theme"><button title="Copy Code" class="copy"></button><span class="lang"></span><pre class="shiki shiki-themes github-light github-dark vp-code"><code><span class="line"><span>.</span></span>
<span class="line"><span>├── Controllers                        </span></span>
<span class="line"><span>│     └── HomeController.cs</span></span>
<span class="line"><span>│     └── AboutController.cs</span></span>
<span class="line"><span>├── Views</span></span>
<span class="line"><span>│     └── Home</span></span>
<span class="line"><span>│     	└── Index.cshtml</span></span>
<span class="line"><span>│     └── About</span></span>
<span class="line"><span>│     	└── Index.cshtml</span></span>
<span class="line"><span>│     └── Shared</span></span>
<span class="line"><span>│     	└── _Layout.cshtml</span></span>
<span class="line"><span>├── appsetting.json                   </span></span>
<span class="line"><span>└── Program.cs</span></span></code></pre></div><p>在项目根目录创建名称为i18n的文件夹，在它下面创建资源文件，资源名称要与页面文件名保持一致</p><div class="language- vp-adaptive-theme"><button title="Copy Code" class="copy"></button><span class="lang"></span><pre class="shiki shiki-themes github-light github-dark vp-code"><code><span class="line"><span>.</span></span>
<span class="line"><span>├── Controllers                        </span></span>
<span class="line"><span>│     └── HomeController.cs</span></span>
<span class="line"><span>│     └── AboutController.cs</span></span>
<span class="line"><span>├── Views</span></span>
<span class="line"><span>│     └── Home</span></span>
<span class="line"><span>│     	└── Index.cshtml</span></span>
<span class="line"><span>│     └── About</span></span>
<span class="line"><span>│     	└── Index.cshtml</span></span>
<span class="line"><span>│     └── Shared</span></span>
<span class="line"><span>│     	└── _Layout.cshtml</span></span>
<span class="line"><span>├── i18n</span></span>
<span class="line"><span>│     └── Views</span></span>
<span class="line"><span>│	     └── Home</span></span>
<span class="line"><span>│	     	└── Index.en-US.json</span></span>
<span class="line"><span>│	     	└── Index.zh-CN.json</span></span>
<span class="line"><span>│	     └── About</span></span>
<span class="line"><span>│	     	└── Index.en-US.json</span></span>
<span class="line"><span>│	     	└── Index.zh-CN.json</span></span>
<span class="line"><span>│	     └── Shared</span></span>
<span class="line"><span>│	     	└── _Layout.en-US.json</span></span>
<span class="line"><span>│	     	└── _Layout.zh-CN.json</span></span>
<span class="line"><span>├── appsetting.json                   </span></span>
<span class="line"><span>└── Program.cs</span></span></code></pre></div><p>这个例子创建了英文和中文两个资源包 不使用文件夹方式创建资源包，用圆点代替的示例</p><div class="language- vp-adaptive-theme"><button title="Copy Code" class="copy"></button><span class="lang"></span><pre class="shiki shiki-themes github-light github-dark vp-code"><code><span class="line"><span>.</span></span>
<span class="line"><span>├── Controllers                        </span></span>
<span class="line"><span>│     └── HomeController.cs</span></span>
<span class="line"><span>│     └── AboutController.cs</span></span>
<span class="line"><span>├── Views</span></span>
<span class="line"><span>│     └── Home</span></span>
<span class="line"><span>│     	└── Index.cshtml</span></span>
<span class="line"><span>│     └── About</span></span>
<span class="line"><span>│     	└── Index.cshtml</span></span>
<span class="line"><span>│     └── Shared</span></span>
<span class="line"><span>│     	└── _Layout.cshtml</span></span>
<span class="line"><span>├── i18n</span></span>
<span class="line"><span>│     └── Views.Home.Index.en-US.json</span></span>
<span class="line"><span>│     └── Views.Home.Index.zh-CN.json</span></span>
<span class="line"><span>│     └── Views.About.Index.en-US.json</span></span>
<span class="line"><span>│     └── Views.About.Index.zh-CN.json</span></span>
<span class="line"><span>│     └── Views.Shared._Layout.en-US.json</span></span>
<span class="line"><span>│     └── Views.Shared._Layout.zh-CN.json</span></span>
<span class="line"><span>├── appsetting.json                   </span></span>
<span class="line"><span>└── Program.cs</span></span></code></pre></div><h2 id="使用国际化服务" tabindex="-1">使用国际化服务 <a class="header-anchor" href="#使用国际化服务" aria-label="Permalink to &quot;使用国际化服务&quot;">​</a></h2><div class="language-csharp vp-adaptive-theme"><button title="Copy Code" class="copy"></button><span class="lang">csharp</span><pre class="shiki shiki-themes github-light github-dark vp-code"><code><span class="line"><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">builder.Services.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">AddJsonLocalization</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">(</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">options</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;"> =&gt;</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">  options.ResourcesPath </span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">=</span><span style="--shiki-light:#032F62;--shiki-dark:#9ECBFF;"> &quot;i18n&quot;</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">); </span><span style="--shiki-light:#6A737D;--shiki-dark:#6A737D;">//i18n是自定义的资源文件夹名称</span></span>
<span class="line"><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">builder.Services.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">AddMvc</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">()</span></span>
<span class="line"><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">    .</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">AddViewLocalization</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">(LanguageViewLocationExpanderFormat.Suffix)</span></span>
<span class="line"><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">    .</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">AddDataAnnotationsLocalization</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">();</span></span></code></pre></div><h2 id="调用国际化中间件" tabindex="-1">调用国际化中间件 <a class="header-anchor" href="#调用国际化中间件" aria-label="Permalink to &quot;调用国际化中间件&quot;">​</a></h2><div class="language-csharp vp-adaptive-theme"><button title="Copy Code" class="copy"></button><span class="lang">csharp</span><pre class="shiki shiki-themes github-light github-dark vp-code"><code><span class="line"><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">var</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;"> app</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;"> =</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;"> builder.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Build</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">();</span></span>
<span class="line"><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">app.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">UseRequestLocalization</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">(</span><span style="--shiki-light:#032F62;--shiki-dark:#9ECBFF;">&quot;zh-CN&quot;</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">, </span><span style="--shiki-light:#032F62;--shiki-dark:#9ECBFF;">&quot;en-US&quot;</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">); </span><span style="--shiki-light:#6A737D;--shiki-dark:#6A737D;">//第一个参数指定的语言包是默认值，切换语言包通过url添加参数?culture=en 实现</span></span></code></pre></div><h2 id="在视图中使用" tabindex="-1">在视图中使用 <a class="header-anchor" href="#在视图中使用" aria-label="Permalink to &quot;在视图中使用&quot;">​</a></h2><div class="language-csharp vp-adaptive-theme"><button title="Copy Code" class="copy"></button><span class="lang">csharp</span><pre class="shiki shiki-themes github-light github-dark vp-code"><code><span class="line"><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">@using Microsoft.AspNetCore.Mvc.Localization</span></span>
<span class="line"><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">@inject IViewLocalizer L</span></span>
<span class="line"></span>
<span class="line"><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">@L[</span><span style="--shiki-light:#032F62;--shiki-dark:#9ECBFF;">&quot;home&quot;</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">]</span></span>
<span class="line"><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">@L[</span><span style="--shiki-light:#032F62;--shiki-dark:#9ECBFF;">&quot;about&quot;</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">]</span></span></code></pre></div>`,14),l=[e];function t(h,o,c,r,d,k){return n(),a("div",null,l)}const u=s(i,[["render",t]]);export{g as __pageData,u as default};
