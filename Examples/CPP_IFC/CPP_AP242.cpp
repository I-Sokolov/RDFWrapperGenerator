
#include "O:\DevArea\RDF\RDFWrappers\bin\Debug\net5.0\AP242.h"
using namespace AP242;

static void test_list3();

extern void AP242_test()
{
    test_list3();
}


template <typename L> static void ASSERT_EQ_LST(L& lst1, L& lst2)
{
    assert(lst1.size() == lst2.size());
    auto it1 = lst1.begin();
    auto it2 = lst2.begin();
    for (; it1 != lst1.end() && it2 != lst2.end(); it1++, it2++) {
        assert(*it1 == *it2);
    }
}


template <typename L> static void ASSERT_EQ_LST2(L& lst1, L& lst2)
{
    assert(lst1.size() == lst2.size());
    auto it1 = lst1.begin();
    auto it2 = lst2.begin();
    for (; it1 != lst1.end() && it2 != lst2.end(); it1++, it2++) {
        ASSERT_EQ_LST(*it1, *it2);
    }
}


template <typename L> static void ASSERT_EQ_LST3(L& lst1, L& lst2)
{
    assert(lst1.size() == lst2.size());
    auto it1 = lst1.begin();
    auto it2 = lst2.begin();
    for (; it1 != lst1.end() && it2 != lst2.end(); it1++, it2++) {
        ASSERT_EQ_LST2(*it1, *it2);
    }
}


static void test_list3()
{
    /// 
    /// Create list of list of list of double
    /// 
    int_t  model = sdaiCreateModelBN(0, NULL, "AP242");
    SetSPFFHeaderItem(model, 9, 0, sdaiSTRING, "AP242");
    SetSPFFHeaderItem(model, 9, 1, sdaiSTRING, 0);

    assert(model);

    auto bspline_volume = rational_b_spline_volume::Create(model);

    list_of_list_of_list_of_double weights;

    bspline_volume.get_weights_data(weights);
    assert(weights.size() == 0);

    for (int i = 0; i < 2; i++) {

        weights.push_back(list_of_list_of_double());
        auto& list2 = weights.back();

        for (int j = 0; j < 3; j++) {

            list2.push_back(list_of_double());
            auto& list = list2.back();

            for (int k = 0; k < 2; k++) {
                list.push_back(100 * i + 10 * j + k);
            }
        }       
    }

    bspline_volume.set_weights_data(weights);

    sdaiSaveModelBN(model, "Test.ap");

    /// 
    /// Now read
    /// 
    auto modelRead = sdaiOpenModelBN(NULL, "Test.ap", "AP242");
    
    auto entity = sdaiGetEntity(modelRead, "RATIONAL_B_SPLINE_VOLUME");
    assert(entity);

    auto volumes = sdaiGetEntityExtent(modelRead, entity);
    auto N_volumes = sdaiGetMemberCount(volumes);
    assert(N_volumes == 1);
    for (int_t i = 0; i < N_volumes; i++) {
     
        int_t volume = 0;
        engiGetAggrElement(volumes, i, sdaiINSTANCE, &volume);

        list_of_list_of_list_of_double weights2;
        rational_b_spline_volume(volume).get_weights_data(weights2);

        ASSERT_EQ_LST3(weights, weights2);
    }
    
    sdaiCloseModel(modelRead);
}