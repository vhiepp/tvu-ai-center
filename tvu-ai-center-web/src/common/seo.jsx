import Head from "next/head";

const SEO = ({ pageTitle }) => (
  <>
    <Head>
      <title>{pageTitle && `${pageTitle} - Trung tâm Trí tuệ nhân tạo`}</title>
      <meta httpEquiv="x-ua-compatible" content="ie=edge" />
      <meta
        name="description"
        content="Trung tâm Trí tuệ nhân tạo - Trường Đại học Trà Vinh"
      />
      <meta name="robots" content="noindex, follow" />
      <meta
        name="viewport"
        content="width=device-width, initial-scale=1, shrink-to-fit=no"
      />
      <link rel="icon" href="/assets/images/favicon/favicon.ico" />
    </Head>
  </>
);

export default SEO;
