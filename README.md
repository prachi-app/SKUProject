# SKUProject
Code given by LifeSeasons

Requirement:
   We want to give our marketing team the ability to make combo offers for various products and set the promo price to be whatever they want it to be without having to create a "Kit" item in our ERP for each combo they make.  Because BigCommerce does not have a native way to combine products for a promo, but write each item as a separate line item, we are using a naming convention that allows us to interpret this "grouping" of products in the correct way.  If marketing wants to combine two products (SKUs 208 and 225 for example,) they will name the grouping G-208-225.  We will read any item in a BigCommerce order that starts with "G-" as a grouping of items that we will write into a Sales Order in Sage as individual line items.  In this example, we would write the SO with 1x 208 and 1x 225.

 

The other consideration is that we need to allocate the revenue for the promo to the items proportionally for accounting purposes.  To do that, we want to use the following math.

 

Write a program using C# that can be fed one of these new Grouping items with the promo price (such as G-208-225 at a price of 44.99) and return each item and the amount of the group price that should be allocated to it in our ERP (Sage.)

 

Below the examples, you will find a list of 50 items by SKU with thier unit cost.  Use this as your reference.

 

Here are rules to keep in mind:

- A Grouping can contain as many items as wanted by Marketing

- Each SKU in a Grouping represents a single (quantity) bottle of that SKU

- A Grouping can contain more than one (quantity) of a single SKU, but must be represented as additional SKU reference - for example G-208-208-208 would be read as 3x SKU 208.

 

 

Here are two examples of how to allocate revenue to items in new "G-" groupings from BigCommerce:

 

 

1st - determine the total unit cost of the items in the order

2nd - determine the % unit cost of the first item rounded to nearest tenth

3rd - if there is more than 1 remaining item (meaning that the items in the order are more than 2) determine the $ unit cost of the next item, rounded to nearest tenth

4th - repeat 3rd step for each additional item until only one item is remaining

5th - using the calculated % of unit cost for each item starting with the first, calculate the % of the group price for the each item except the last, rounded to the nearest tenth.

6th - subtract each calculated price from the total item (group) price to find the amount to allocate to the last item.

 

 

Example 1 - 2 Items in Group -

G-208-225 comes across as a line item at price of 44.99

 

Unit Cost of 208 is 6.82 and Retail Price is 41.99

Unit Cost of 225 is 1.94 and Retail Price is 9.99

1st - determine the total unit cost of the items in the order

Total Unit Cost of G-208-221 is 8.76 and total retail price is 51.98

2nd - determine the % unit cost of the first item rounded to nearest tenth

208 is 78% of the total unit cost (6.82/8.76 = .7785...  round to nearest tenth = .78 or 78%)

5th - using the calculated % of unit cost for each item starting with the first, calculate the % of the group price for the each item except the last, rounded to the nearest tenth.

208 should be entered into the SO at price of $35.09 (44.99 * .78 = 35.0922  round to nearest tenth = 35.09)

6th - subtract each calculated price from the total item (group) price to find the amount to allocate to the last item.

225 should be entered into the SO at price of $9.90 (44.99 - 35.09 = 9.90)

 

 

 

Example 2 - 4 Items in Group -

G-208-225-237-258 comes across as a line item at price of 89.00

 

Unit Cost of 208 is 6.82 and Retail Price is 41.99

Unit Cost of 225 is 1.94 and Retail Price is 9.99

Unit Cost of 237 is 1.73 and Retail Price is 9.99

Unit Cost of 258 is 8.02 and Retail Price is 46.99

1st - determine the total unit cost of the items in the order

Total Unit Cost of G-208-221 is 18.51 and total retail price is 108.96

2nd - determine the % unit cost of the first item rounded to nearest tenth

208 is 37% of the total unit cost (8.76/18.51 = .36844...  round to nearest tenth = .37 or 37%)

3rd - if there is more than 1 remaining item (meaning that the items in the order are more than 2) determine the $ unit cost of the next item, rounded to nearest tenth

225 is 10% of the total unit cost (1.94/18.51 = .10480...  round to nearest tenth = .10 or 10%)

4th - repeat 3rd step for each additional item until only one item is remaining

237 is  9% of the total unit cost (1.73/18.51 = .09346...  round to nearest tenth = .09 or  9%)

5th - using the calculated % of unit cost for each item starting with the first, calculate the % of the group price for the each item except the last, rounded to the nearest tenth.

208 should be entered into the SO at price of $32.93 (89.00 * .37 = 32.93  round to nearest tenth = 32.93)

225 should be entered into the SO at price of $08.90 (89.00 * .10 = 08.90  round to nearest tenth = 08.90)

237 should be entered into the SO at price of $08.01 (89.00 * .09 = 08.01  round to nearest tenth = 08.01)

6th - subtract each calculated price from the total item (group) price to find the amount to allocate to the last item.

258 should be entered into the SO at price of $39.16 (89.00 - 32.93 - 8.90 - 8.01 = 39.16)

 

 

SKU        Unit Cost

201         2.95

202         6.96

203         5.53

204         2.86

205         6.49

206         5.64

207         7.02

208         4.26

209         7.33

210         5.11

211         7.02

212         5.71

213         2.88

214         3.92

215         5.68

216         4.71

217         4.22

218         4.13

219         5.23

220         4.96

221         7.68

222         5.73

223         4.36

224         4.00

225         4.13

226         7.65

227         5.11

228         6.09

229         3.08

230         4.41

231         4.66

232         7.65

233         6.81

234         6.75

235         4.75

236         5.47

237         8.04

238         5.30

239         6.46

240         5.01

241         6.91

242         8.04

243         6.19

244         3.31

245         8.20

246         3.56

247         6.61

248         5.91

249         4.43

250         8.05
