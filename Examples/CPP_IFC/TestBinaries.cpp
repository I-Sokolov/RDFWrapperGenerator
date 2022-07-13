
#include "O:\DevArea\RDF\RDFWrappers\bin\Debug\net5.0\IFC4.h"

using namespace IFC4;

extern void TestBinaries()
{
#define NC 4
    char rasterCode[NC * 1024 * 4 + 2];
    rasterCode[0] = '1';
    rasterCode[NC * 1024 * 4] = '8';
    rasterCode[NC * 1024 * 4 + 1] = 0;
    for (int i = 1; i < NC * 1024 * 4; i++) {
        rasterCode[i] = 'A' + i % 3;
    }

    int_t  ifcModel = sdaiCreateModelBN(0, NULL, "IFC4");
    SetSPFFHeaderItem(ifcModel, 9, 0, sdaiSTRING, "IFC4");
    SetSPFFHeaderItem(ifcModel, 9, 1, sdaiSTRING, 0);

    //
    //scalar attribute
    auto blobTexture = IfcBlobTexture::Create(ifcModel);
    blobTexture.put_RepeatS(true);
    blobTexture.put_RepeatT(true);
    blobTexture.put_RasterFormat("PNG");
    blobTexture.put_Mode("MODULATE");

    assert(blobTexture.get_RasterCode() == NULL);
    blobTexture.put_RasterCode(rasterCode);
    assert(0 == strcmp(blobTexture.get_RasterCode(), rasterCode));

    //put/get with SDAI
    sdaiPutAttrBN(blobTexture, "RasterCode", sdaiBINARY, rasterCode);
    IfcBinary gotData = NULL;
    sdaiGetAttrBN(blobTexture, "RasterCode", sdaiBINARY, &gotData);
    assert(!strcmp(gotData, rasterCode));

    //can also get as string
    sdaiGetAttrBN(blobTexture, "RasterCode", sdaiSTRING, &gotData);
    assert(!strcmp(gotData, rasterCode));

    //
    //aggregation
    auto pixelTexture = IfcPixelTexture::Create(ifcModel);

    ListOfIfcBinary lstBin;
    pixelTexture.get_Pixel(lstBin);
    assert(lstBin.size() == 0);

    lstBin.push_back(rasterCode);
    lstBin.push_back(rasterCode);

    pixelTexture.put_Pixel(lstBin);
    
    lstBin.clear();
    pixelTexture.get_Pixel(lstBin);
    assert(lstBin.size() == 2 && !strcmp(lstBin.front().c_str(), rasterCode) && !strcmp(lstBin.back().c_str(), rasterCode));

    //
    //select
    auto value = IfcAppliedValue::Create(ifcModel);

    auto bin = value.get_AppliedValue().get_IfcValue().get_IfcSimpleValue().get_IfcBinary();
    assert(bin == 0);

    value.put_AppliedValue().put_IfcValue().put_IfcSimpleValue().put_IfcBinary(rasterCode);
    bin = value.get_AppliedValue().get_IfcValue().get_IfcSimpleValue().get_IfcBinary();
    assert(!strcmp(bin, rasterCode));

    //simplified form
    bin = value.get_AppliedValue().as_text();
    assert(!strcmp(bin, rasterCode));

    //
    //save and read

    sdaiSaveModelBN(ifcModel, "Binaries.ifc");
    sdaiCloseModel(ifcModel);

    //
    // Re-read
    //
    ifcModel = sdaiOpenModelBN(NULL, "Binaries.ifc", "IFC4");

    auto entityBlobTexture = sdaiGetEntity(ifcModel, "IfcBlobTexture");
    auto blobTextureAggr = sdaiGetEntityExtent(ifcModel, entityBlobTexture);
    auto N = sdaiGetMemberCount(blobTextureAggr);
    assert(N == 1);
    for (int_t i = 0; i < N; i++) {
        int_t inst = 0;
        engiGetAggrElement(blobTextureAggr, i, sdaiINSTANCE, &inst);
        auto code = IfcBlobTexture(inst).get_RasterCode();
        assert(0 == strcmp(code, rasterCode));
    }

    auto entityPixelTexture = sdaiGetEntity(ifcModel, "IfcPixelTexture");
    auto pixelTextureAggr = sdaiGetEntityExtent(ifcModel, entityPixelTexture);
    N = sdaiGetMemberCount(pixelTextureAggr);
    assert(N == 1);
    for (int_t i = 0; i < N; i++) {
        int_t inst = 0;
        engiGetAggrElement(pixelTextureAggr, i, sdaiINSTANCE, &inst);
        ListOfIfcBinary lstBin;
        IfcPixelTexture(inst).get_Pixel(lstBin);
        assert(lstBin.size() == 2 && !strcmp(lstBin.front().c_str(), rasterCode) && !strcmp(lstBin.back().c_str(), rasterCode));
    }

    auto entityValue = sdaiGetEntity(ifcModel, "IfcAppliedValue");
    auto valueAggr = sdaiGetEntityExtent(ifcModel, entityValue);
    N = sdaiGetMemberCount(pixelTextureAggr);
    assert(N == 1);
    for (int_t i = 0; i < N; i++) {
        int_t inst = 0;
        engiGetAggrElement(valueAggr, i, sdaiINSTANCE, &inst);
        auto v = IfcAppliedValue(inst).get_AppliedValue().get_IfcValue().get_IfcSimpleValue().get_IfcBinary();
        assert(!strcmp(v, rasterCode));
    }

    sdaiCloseModel(ifcModel);

}

