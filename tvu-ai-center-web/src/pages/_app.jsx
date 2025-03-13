import MainLayout from "@/layout/main";
import "../styles/index.scss";

if (typeof window !== "undefined") {
  require("bootstrap/dist/js/bootstrap");
}

export default function App({ Component, pageProps }) {
  const Layout = Component.Layout || MainLayout;

  return (
    <Layout>
      <Component {...pageProps} />
    </Layout>
  );
}
