import React, { useEffect, useState } from "react";
import { useTranslation } from "@/utils/i18n";
import { domain } from "@/apis/apiClient";

const Brand = () => {
  const { t } = useTranslation();
  const [data, setData] = useState(null);

  const fetchPartner = async () => {
    const fData = await fetch(`${domain}/partners`);
    const res = await fData.json();
    // console.log(data);
    setData(res.data);
  };

  useEffect(() => {
    fetchPartner();
  }, []);

  return (
    <>
      <div className="ab-brand-area">
        <div className="container">
          <div className="ab-brand-border-bottom pb-90">
            <div className="row">
              <div className="col-12">
                <div className="ab-brand-section-box text-center mb-50">
                  <h4 className="ab-brand-title">{t("partner")}</h4>
                  <p>{t("partner_description")}</p>
                </div>
              </div>
            </div>
            <div className="row justify-content-center">
              <div className="col-xl-10">
                <div className="row row-cols-2 row-cols-sm-3 row-cols-md-4 row-cols-lg-5 justify-content-center">
                  {data &&
                    data.map((item, i) => (
                      <div
                        key={i}
                        className="col wow tpfadeUp"
                        style={{
                          width: "174px",
                          height: "84px",
                        }}
                        data-wow-duration=".9s"
                        data-wow-delay={".2s"}
                      >
                        <div
                          className="ab-brand-item mb-25"
                          style={{ width: "100%", height: "100%" }}
                        >
                          <img
                            src={item.logo}
                            alt={item.name}
                            style={{
                              width: "100%",
                              height: "100%",
                              objectFit: "cover",
                              objectPosition: "center",
                              boxShadow: "0 0 10px rgba(0, 0, 0, 0.3)",
                            }}
                          />
                        </div>
                      </div>
                    ))}
                </div>
                {data && data.length === 0 && (
                  <div className="text-center">
                    <p style={{ fontWeight: "bold", fontSize: "1.2rem" }}>
                      To be updated
                    </p>
                  </div>
                )}
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default Brand;
