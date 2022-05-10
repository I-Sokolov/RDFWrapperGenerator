// CPP_GeometryKernel.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <stdio.h>
#include <math.h>
#include <string.h>
#include <assert.h>


#include "Engine\engine.h"
#include "..\GeometryKernel.h"
using namespace GeometryKernel;

static int64_t CreateRedBox(int64_t model);
static void MoreExamplesToAccessDifferentTypesOfProperties(int64_t model);

/// <summary>
/// 
/// </summary>
/// <returns></returns>
int main()
{
	auto model = OpenModel(nullptr);

	CreateRedBox(model);

	MoreExamplesToAccessDifferentTypesOfProperties(model);

	CloseModel(model);

	printf("finsihed successfully\n");
}


/// <summary>
/// 
/// </summary>
/// <param name="model"></param>
/// <returns></returns>
static int64_t CreateRedBox(int64_t model)
{
	//
	// create colored material
	//

	ColorComponent colorComponent = ColorComponent::Create(model);
	colorComponent.set_R(0.9);
	colorComponent.set_G(0);
	colorComponent.set_B(0);

	//you can use instance and property handlers API
	int64_t propW = GetPropertyByName(model, "W");
	double w = 0.5;
	SetDatatypeProperty(colorComponent, propW, &w, 1);
	//the code above is equivalent to
	colorComponent.set_W(0.5);

	//or you easy use existing instance handlers with classes
	int64_t colorClass = GetClassByName(model, "Color");
	int64_t colorInstance = CreateInstance(colorClass, nullptr);

	//get wrapper object from instance handler
	Color color(colorInstance);
	color.set_ambient(colorComponent);

	Material material = Material::Create(model);
	material.set_color(color);

	//
	Box box = Box::Create(model);

	box.set_height(3);
	box.set_width(2);
	box.set_length(4);
	box.set_material(material);  //set_material is inherited from GeometricItem

	return box;
}

///
#define ASSERT_ARR_EQ(r1,r2,N)  	for (int i=0; i<N; i++) { ASSERT(fabs ((double)r1[i]-(double)r2[i]) < 1e-9);}

/// <summary>
/// 
/// </summary>
/// <param name=""></param>
static void MoreExamplesToAccessDifferentTypesOfProperties(int64_t model)
{
	//teste to set/get different property types

	Texture texture = Texture::Create(model);
	auto curve = NURBSCurve::Create(model);

	//double
	double* lseg = curve.get_segmentationLength();
	ASSERT(lseg == null);
	curve.set_segmentationLength(0.5);
	lseg = curve.get_segmentationLength();
	ASSERT(*lseg == 0.5);

	//double []
	int64_t cnt;
	double* org = texture.get_origin(&cnt);
	ASSERT(org == null);
	double orgset[] = {1, 2, 3};
	texture.set_origin(orgset, 3);
	org = texture.get_origin(&cnt);
	ASSERT(cnt == 3);
	ASSERT_ARR_EQ(org, orgset, cnt);

	//long
	long* setting = curve.get_setting();
	ASSERT(setting == null);
	curve.set_setting(13);
	setting = curve.get_setting();
	ASSERT(*setting == 13);

	//long[]
	long* km = curve.get_knotMultiplicities(&cnt);
	ASSERT(km == null);
	long kmset[] = {3, 5, 6};
	curve.set_knotMultiplicities(kmset, 3);
	km = curve.get_knotMultiplicities(&cnt);
	ASSERT(cnt == 3);
	ASSERT_ARR_EQ(km, kmset, cnt);

	//string 
	const char** tname = texture.get_name();
	ASSERT(tname == null);
	texture.set_name("test");
	tname = texture.get_name();
	ASSERT(0 == strcmp(*tname, "test"));

	//string[]
	//no example in Geometry Kernel

	//bool
	bool* closed = curve.get_closed();
	ASSERT(closed == null);
	curve.set_closed(true);
	closed = curve.get_closed();
	ASSERT(closed && *closed);

	//bool[]
	//no example in Geometry Kernel

	//object
	Material* material = curve.get_material();
	ASSERT(material == null);
	int64_t mat = Material::Create(model);
	curve.set_material(Material(mat));
	material = curve.get_material();
	ASSERT(*material == mat);
	Material* m2 = curve.get_material();
	ASSERT(*m2 == *material);

	//object []
	Point3D* ptg = curve.get_controlPoints(&cnt);
	ASSERT(ptg == null);
	int64_t* ptg64 = curve.get_controlPoints_int64(&cnt);
	ASSERT(ptg64 == null);

	Point3D pts[] = {Point3D::Create(model), Point3D::Create(model)};
	ASSERT(pts[0] != pts[1]);

	curve.set_controlPoints(pts, 2);

	ptg = curve.get_controlPoints(&cnt);
	ASSERT(cnt == 2);
	for (int i = 0; i < cnt; i++) ASSERT(pts[i] == ptg[i]);
	ASSERT_ARR_EQ(ptg, pts, cnt);

	ptg64 = curve.get_controlPoints_int64(&cnt);
	ASSERT(cnt == 2);
	for (int i = 0; i < cnt; i++) ASSERT(pts[i] == ptg64[i]);
	ASSERT_ARR_EQ(ptg64, pts, cnt);
}
